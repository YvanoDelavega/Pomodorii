using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pomodorii.Models
{
    public class Semi
    {
        /// <summary>
        /// id d'un semi
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// id de la tomate concernée
        /// </summary>
        public int TomateId { get; set; }

        /// <summary>
        /// date du semi
        /// </summary>
        public DateTime Date { get; set; }
    }
}
