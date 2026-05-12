namespace Ahorcado
{
    public class PalabrasEnMemoria : IRepositorioPalabras
    {
        private readonly Dictionary<string, List<string>> _palabrasPorCategoria = new()
        {
            {
                "arquitectura",
                new List<string>
                {
                    "arquitectura",
                    "componente",
                    "descomposicion",
                    "dependencia",
                    "acoplamiento"
                }
            },
            {
                "poo",
                new List<string>
                {
                    "polimorfismo",
                    "encapsulamiento",
                    "herencia",
                    "abstraccion",
                    "clase"
                }
            },
            {
                "net",
                new List<string>
                {
                    "ensamblado",
                    "namespace",
                    "interfaz",
                    "delegado",
                    "middleware"
                }
            }
        };

        private readonly string _categoria;

        public PalabrasEnMemoria(string categoria)
        {
            _categoria = categoria.ToLower();
        }

        public string ObtenerPalabraAleatoria()
        {
            var random = new Random();

            string categoriaValida = _palabrasPorCategoria.ContainsKey(_categoria)
                ? _categoria
                : "arquitectura";

            var palabras = _palabrasPorCategoria[categoriaValida];

            return palabras[random.Next(palabras.Count)];
        }
    }
}