using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Application.DTOs.Attachments
{
    public class FileDownloadDto
    {

        // Nom affiché lors du téléchargement
        public string FileName { get; set; } = null!;



        // Type du fichier
        // exemple:
        // application/pdf
        // image/png
        public string ContentType { get; set; } = null!;



        // Contenu du fichier
        public byte[] Content { get; set; } = null!;


    }
}
