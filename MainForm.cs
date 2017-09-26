using AtmServer.AppCode;
using AtmServer.AppCode.Controllers;
using AtmServer.AppCode.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace AtmServer
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
        private List<string> users = new List<string>();
        private static Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private MainFormController _controller;
        private LogController _logController;
        public static string JsonRequest { get; set; }

        delegate void SetTextCallback(string text);
        delegate void AddUserCallback(string text);
        #region UpdateUI
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
        private void UpdateUsersConnected()
        {
            Invoke((MethodInvoker)(delegate
            {
                lboxUsuariosConectados.DataSource = users;
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
                    users.Remove(jsonSimpleRequest.Credentials.CustomerNumber);
                    UpdateUsersConnected();
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
                                users.Add(jsonSimpleRequest.Credentials.CustomerNumber);
                                UpdateUsersConnected();
                            }
                            break;
                        case "Cambiar Pin":
                            _controller.ChangePin();
                            jsonResponse = _controller.JsonResponse;
                            break;
                        case "Actualizar Telefono":
                            _controller.UpdateTelefono();
                            jsonResponse = _controller.JsonResponse;
                            break;
                        case "Consultar Usuario":
                            _controller.GetCustomer();
                            jsonResponse = _controller.JsonResponse;
                            break;
                    }
                    break;
                
                case "AccountService":
                    _controller.SetJsonRequest(jsonSimpleRequest);
                    switch (jsonSimpleRequest.Action)
                    {
                            case "Consultar Saldo":
                            _controller.GetAccount();
                            jsonResponse = _controller.JsonResponse;
                            break;

                            case "Retirar Efectivo":
                            _controller.Withdrawal();
                            jsonResponse = _controller.JsonResponse;
                            break;

                            case "Depositar Efectivo":
                            _controller.Deposit();
                            jsonResponse = _controller.JsonResponse;
                            break;

                            case "Transferir":
                            _controller.Transfer();
                            jsonResponse = _controller.JsonResponse;
                            break;
                    }
                    break;

                case "ProductService":
                    _controller.SetJsonRequest(jsonSimpleRequest);
                    switch (jsonSimpleRequest.Action)
                    {
                        case "Consultar Productos":
                            _controller.GetAllProductsByCustomer();
                            jsonResponse = _controller.JsonResponse;
                            break;
                        case "Pagar Producto":
                            _controller.PayProduct();
                            jsonResponse = _controller.JsonResponse;
                            break;
                    }
                    break;
                case "LogService":
                    _controller.SetJsonRequest(jsonSimpleRequest);
                    switch (jsonSimpleRequest.Action)
                    {
                        case "Consultar Bitacora":
                            _controller.GetAllLogByCustomer();
                            jsonResponse = _controller.JsonResponse;
                            break;
                    }
                    break;

                default:
                    jsonResponse.MessageResult = "Invalid Request";
                    break;

            }
            UpdateBitacora();
            
            var json = JsonConvert.SerializeObject(jsonResponse, Formatting.Indented);
            var encryptSendText = cryptoClass.Encriptar(json);
                UpdateTramaJsonSaliente(json, encryptSendText);
            var data = Encoding.ASCII.GetBytes(encryptSendText);

            socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);

        }

        private void SendCallback(IAsyncResult ar)
        {
            var socket = (Socket)ar.AsyncState;
            socket.EndSend(ar);
        }
    }
}
