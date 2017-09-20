using FtpServerUI.AppCode;
using FtpServerUI.AppCode.Controllers;
using FtpServerUI.AppCode.Dto;
using FtpServerUI.AppCode.DtoModels;
using FtpServerUI.AppCode.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FtpServerUI
{
    public partial class MainForm : Form
    {
        class Client
        {
            public string Username { get; set; }
            public IPEndPoint IdSocket { get; set; }
            public Socket Socket { get; set; }

        }
        private static int BUFFER_SIZE = 214748364;
        private static byte[] _buffer = new byte[BUFFER_SIZE];
        private static List<Socket> _clientSockets = new List<Socket>();
        private static List<Client> _clients = new List<Client>();
        private static Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private MainFormController _controller;
        private LogController _logController;
        public static string JsonRequest { get; set; }

        delegate void SetTextCallback(string text);
        delegate void AddUserCallback(string text);
        #region UpdateUI
        private void SetRichText(string text)
        {
            if (rtTramaEntranteDesc.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetRichText);
                Invoke(d, new object[] { text });
            }
            else
            {
                rtTramaEntranteDesc.Text = text;
            }
        }
        private void UpdateTramaJsonEntrante(string json, string encrypt)
        {
            Invoke((MethodInvoker)(delegate
            {
                rtTramaEntranteDesc.Text = json;
            }));

            Invoke((MethodInvoker)(delegate
            {
                rtTramaEntranteEncrypt.Text = encrypt;
            }));
        }
        private void UpdateHash(string cliente, string server)
        {
            Invoke((MethodInvoker)(delegate
            {
                lbHashClient.Text = cliente;
            }));

            Invoke((MethodInvoker)(delegate
            {
                lbHashServer.Text = server;
            }));
        }
        private void UpdateTramaJsonSaliente(string json, string encrypt)
        {
            Invoke((MethodInvoker)(delegate
            {
                rtTramaSalienteDesc.Text = json;
            }));

            Invoke((MethodInvoker)(delegate
            {
                rtTramaSalienteEncrypt.Text = encrypt;
            }));

        }
        private void UpdateBitacora()
        {
            Invoke((MethodInvoker)(delegate
            {
                dgBitacora.DataSource = _logController.GetAll();
            }));

        }
        private void AddUserConnected(string user)
        {
            Invoke((MethodInvoker)(delegate
            {
                lboxUsuariosConectados.Items.Add(user);
            }));
        }

        #endregion
        public MainForm()
        {
            InitializeComponent();
            SetupServer();
            _controller = new MainFormController();
            _logController = new LogController();
            dgBitacora.DataSource = _logController.GetAll();
        }

        private void SetupServer()
        {
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, 100));
            _serverSocket.Listen(5);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private void AcceptCallback(IAsyncResult AR)
        {
            var socket = _serverSocket.EndAccept(AR);
            _clientSockets.Add(socket);
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private void ReceiveCallback(IAsyncResult AR)
        {
            var socket = (Socket)AR.AsyncState;
            if (!socket.Connected) return;
            var received = socket.EndReceive(AR);
            var dataBuf = new byte[received];
            var idClient = socket.RemoteEndPoint.ToString();
            var cryptoClass = new CryptographyObject(idClient);
            Array.Copy(_buffer, dataBuf, received);

            var encryptText = Encoding.ASCII.GetString(dataBuf);
            JsonRequest = cryptoClass.Desencriptar(encryptText);

            UpdateTramaJsonEntrante(JsonRequest, encryptText);
            
            var jsonSimpleRequest = JsonConvert.DeserializeObject<JsonRequest>(JsonRequest);
            var serverHash = cryptoClass.Md5Gen();

            UpdateHash(jsonSimpleRequest.Credentials.Hash, serverHash);

            var jsonResponse = new JsonResponse();

            #region ValidateHash
                if (jsonSimpleRequest.Credentials.Hash != serverHash)
                {
                    MessageBox.Show("El cliente ha creado una petición con una llave inválida.");
                    return;
                }
                if (jsonSimpleRequest == null)
                {
                    MessageBox.Show("Ha ocurrido un error con la petición del cliente");
                    return;
                }
                if (jsonSimpleRequest.Action == "Desconectar del sistema")
                {
                    MessageBox.Show("Usuario se ha desconectado.");
                    return;
                }
            #endregion

            switch (jsonSimpleRequest.Service)
            {
                case "CustomerService":
                    _controller.SetJsonRequest(jsonSimpleRequest);
                    switch (jsonSimpleRequest.Action)
                    {
                        case "Iniciar Sesion":
                            _controller.Login();
                            jsonResponse = _controller.JsonResponse;
                            if (_controller.JsonResponse.MessageResult == "Autorizado")
                            {
                                AddUserConnected(jsonSimpleRequest.Credentials.CustomerNumber);
                            }
                            break;
                    }
                    break;
                
                case "AccountService":
                    _controller.SetJsonRequest(jsonSimpleRequest);
                    switch (jsonSimpleRequest.Action)
                    {
                            case "Seleccionar Cuenta":
                            _controller.GetAccount();
                            jsonResponse = _controller.JsonResponse;
                            break;
                    }
                    break;

                default:
                    jsonResponse.MessageResult = "Invalid Request";
                    break;

            }
            UpdateBitacora();
            //userController.Username = jsonSimpleRequest.Credentials.Username;
            //userController.Accion = jsonSimpleRequest.Action;

            //userController.Login(jsonSimpleRequest.Credentials);
            //if (userController.MessageResult == "1") AddUserConnected(userController.Username);

            //SetRichText( JsonRequest );
            //var response = string.Empty;

            //if (text.ToLower() != "get time")
            //{
            //    response = "Invalid Request";
            //}
            //else
            //{
            //    response = DateTime.Now.ToLongDateString();
            //}

            var json = JsonConvert.SerializeObject(jsonResponse, Formatting.Indented);
            var encryptSendText = cryptoClass.Encriptar(json);
                UpdateTramaJsonSaliente(json, encryptSendText);
            var data = Encoding.ASCII.GetBytes(encryptSendText);

            socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);

        }

        private void ReceiveCallbackNo3DES(IAsyncResult AR)
        {
            var socket = (Socket)AR.AsyncState;
            if (!socket.Connected) return;
            var received = socket.EndReceive(AR);
            var dataBuf = new byte[received];
            var idClient = socket.RemoteEndPoint.ToString();
            var cryptoClass = new CryptographyObject(idClient);
            Array.Copy(_buffer, dataBuf, received);

            //var encryptText = 
            JsonRequest = Encoding.ASCII.GetString(dataBuf);

            UpdateTramaJsonEntrante(JsonRequest, JsonRequest);

            var jsonSimpleRequest = JsonConvert.DeserializeObject<JsonRequest>(JsonRequest);
            var serverHash = cryptoClass.Md5Gen();

            //UpdateHash(jsonSimpleRequest.Credentials.Hash, serverHash);

            var jsonResponse = new JsonResponse();

            if (jsonSimpleRequest == null)
            {
                MessageBox.Show("Ha ocurrido un error con la petición del cliente");
                return;
            }
            if (jsonSimpleRequest.Action == "Desconectar del sistema")
            {
                MessageBox.Show("Usuario se ha desconectado.");
                return;
            }
            //switch (jsonSimpleRequest.Service)
            //{
            //    case "CustomerService":
            //        var customerController = new CustomerService(jsonSimpleRequest);
            //        switch (jsonSimpleRequest.Action)
            //        {
            //            case "Iniciar Sesion":
            //                AddUserConnected(jsonSimpleRequest.Credentials.CustomerNumber);
            //                jsonResponse.Credentials = customerController.JsonRequest.Credentials;
            //            break;
            //        }
            //        break;

            //    default:
            //        jsonResponse.MessageResult = "Invalid Request";
            //        break;

            //}
            //userController.Username = jsonSimpleRequest.Credentials.Username;
            //userController.Accion = jsonSimpleRequest.Action;

            //userController.Login(jsonSimpleRequest.Credentials);
            //if (userController.MessageResult == "1") AddUserConnected(userController.Username);

            //SetRichText( JsonRequest );
            //var response = string.Empty;

            //if (text.ToLower() != "get time")
            //{
            //    response = "Invalid Request";
            //}
            //else
            //{
            //    response = DateTime.Now.ToLongDateString();
            //}

            var json = JsonConvert.SerializeObject(jsonResponse, Formatting.Indented);
            var encryptSendText = cryptoClass.Encriptar(json);
            UpdateTramaJsonSaliente(json, encryptSendText);
            //var data = Encoding.ASCII.GetBytes(encryptSendText);
            var data = Encoding.ASCII.GetBytes(json);

            socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);

        }
        private void SendCallback(IAsyncResult ar)
        {
            var socket = (Socket)ar.AsyncState;
            socket.EndSend(ar);
        }

        public static string Encriptar(string texto)
        {
            try
            {
                string key = "ftpSecure"; //llave para encriptar datos
                byte[] keyArray;
                byte[] Arreglo_a_Cifrar = UTF8Encoding.UTF8.GetBytes(texto);
                //Se utilizan las clases de encriptación MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
                //Algoritmo TripleDES
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
                tdes.Clear();
                //se regresa el resultado en forma de una cadena
                texto = Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return texto;
        }
        public static string Desencriptar(string textoEncriptado)
        {
            try
            {
                string key = textoEncriptado;
                byte[] keyArray;
                byte[] Array_a_Descifrar = Convert.FromBase64String(textoEncriptado);
                //algoritmo MD5
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);
                tdes.Clear();
                textoEncriptado = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return textoEncriptado;
        }
    }
}
