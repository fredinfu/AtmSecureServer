using FtpServerUI.AppCode.Context;
using FtpServerUI.AppCode.Dto;
using FtpServerUI.AppCode.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpServerUI.AppCode.Controllers
{
    class FileController : ControllerBase
    { 
        
        public FileController()
        {
            _context = new FtpSecureNetworkDataContext();
        }

        public FileController(JsonRequest json)
        {
            JsonRequest = json;
            _context = new FtpSecureNetworkDataContext();
        }
        public List<FileDto> GetSharedFiles()
        {
            var mySharedFiles = _context.FileShares
                .Where(w => w.SharedToUsername == JsonRequest.Credentials.Username)
                .ToList();
            var files = new List<decimal>();
            foreach(var file in mySharedFiles)
            {
                files.Add(file.IdFile);
            }
            var res = _context.Files
                .Where(w => files.Contains(w.IdFile))
                .Select(s => new FileDto
                {
                    IdFile = s.IdFile,
                    FileName = s.FileName,
                    CreatedByUsername = s.CreatedByUsername,
                    CreatedDate = s.CreatedDate

                }).ToList();
            Result = res == null ? "No existen archivos compartidos hacia usted" : "El usuario ha consultado sus archivos compartidos";
            LlenarBitacora();
            return res;

        }

        public List<FileDto> GetUserFiles()
        {
            var res = _context.Files
                .Where(w => w.CreatedByUsername == JsonRequest.Credentials.Username)
                .Select(s => new FileDto
                {
                    IdFile = s.IdFile,
                    FileName = s.FileName,
                    CreatedByUsername = s.CreatedByUsername,
                    CreatedDate = s.CreatedDate

                }).ToList();
            Result = res == null ? "Ocurrió un error al consultar los datos" : "El usuario ha consultado sus archivos";
            LlenarBitacora();
            return res;
        }

        public void UploadFile()
        {
            try
            {
                var file = new File
                {
                    FileName = JsonRequest.FileAux.FileName,
                    CreatedByUsername = JsonRequest.Credentials.Username,
                    CreatedBy = 0,
                    CreatedDate = DateTime.Now
                };

                _context.Files.InsertOnSubmit(file);
                Result = $"Archivo: {JsonRequest.FileAux.FileName} subido exitosamente";
                LlenarBitacora();
            }
            catch (Exception ex)
            {
                Result = "Error";
            }
        }

        public void UploadFileData()
        {
            try
            {
                var file = new File
                {
                    FileName = JsonRequest.FileAux.FileName,
                    FileData = JsonRequest.FileAux.FileData,
                    CreatedByUsername = JsonRequest.Credentials.Username,
                    CreatedBy = 0,
                    CreatedDate = DateTime.Now
                };

                _context.Files.InsertOnSubmit(file);
                Result = $"Archivo: {JsonRequest.FileAux.FileName} subido exitosamente";
                LlenarBitacora();
            }
            catch (Exception ex)
            {
                Result = "Error";
            }
        }
    }
}
