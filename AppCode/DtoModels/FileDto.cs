using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpServerUI.AppCode.DtoModels
{
    public class FileDto
    {
        public FileDto()
        {
            IsDeleted = false;
            CreatedDate = DateTime.Now;
        }
        public decimal IdFile { get; set; }
        public string FileName { get; set; }
        public string FileData { get; set; }
        public string Size { get; set; }
        public bool IsDeleted { get; set; }
        public decimal CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUsername { get; set; }
        //public Binary FileData { get; set; }
    }
}
