
using System.ComponentModel.DataAnnotations;

namespace Pomodorii.Models
{
    public class Tomate
    {
        /// <summary>
        /// id d'une tomate
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// nom de la variété
        /// </summary>
        [Required, MaxLength(50, ErrorMessage = "50 charactères max")]
        public string Nom { get; set; } = "";

        /// <summary>
        /// description succinte
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        ///  sa photo
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        ///  image par défaut
        /// </summary>
        public string? ImageUrlDefaut { get { return !string.IsNullOrWhiteSpace(ImageUrl) ? ImageUrl : Constants.IMG_DEFAULT; } }

    }
}