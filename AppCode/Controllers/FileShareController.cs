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
    class FileShareController : ControllerBase
    {
        
        public FileShareController()
        {
            _context = new FtpSecureNetworkDataContext();
        }

        public FileShareController(JsonRequest json)
        {
            JsonRequest = json;
            _context = new FtpSecureNetworkDataContext();
        }

        public void ShareFile()
        {
            try
            {
                var fileShare = new FileShare
                {
                    IdFile = JsonRequest.FileAux.IdFile,
                    SharedToUsername = JsonRequest.UserAux.Username,
                };

                _context.FileShares.InsertOnSubmit(fileShare);
                LlenarBitacora();
                Result = $"Archivo: {JsonRequest.FileAux.FileName} compartido con: {JsonRequest.UserAux.Username} exitosamente";
            }
            catch (Exception ex) {
                Result = "Error";
            }
        }
        public List<FileDto> GetUserFiles()
        {
            var res = _context.Files
                .Where(w => w.CreatedByUsername == JsonRequest.Credentials.CustomerNumber)
                .Select(s => new FileDto
                {
                    IdFile = s.IdFile,
                    FileName = s.FileName,
                    CreatedByUsername = s.CreatedByUsername,
                    CreatedDate = s.CreatedDate

                }).ToList();

            return res;
        }
    }
}
