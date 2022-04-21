namespace Pomodorii.Models
{
    public enum TomateCouleur
    {
        None = 0,
        Rouge = 1,
        Verte = 2,
        Jaune = 3,
        Noire = 4,
        Orange = 5
    }

    public enum TomateType
    {
        None = 0,
        Cerise = 1,
        Grosse = 2,
        Allongee = 3,
        Normale = 4
    }


    public static class Constants
    {
        public static readonly string API_NAME = "Api";
        public static readonly string IMG_DEFAULT = "/img/TomateNew.png";
    }
}
