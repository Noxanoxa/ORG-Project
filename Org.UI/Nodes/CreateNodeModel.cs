using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.UI.Nodes
{
    internal class CreateNodeModel
    {
        [Required(ErrorMessage = "Le code est obligatoire")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        public string Nom { get; set; }
    }
}