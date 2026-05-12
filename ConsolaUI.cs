namespace Ahorcado
{
    public class ConsolaUI
    {
        private readonly MotorAhorcado _motor;

        public ConsolaUI(MotorAhorcado motor)
        {
            _motor = motor;
        }

        public void MostrarTablero()
        {
            Console.Clear();
            MostrarAhorcado();

            Console.WriteLine($"Intentos restantes: {_motor.IntentosRestantes}");
            Console.WriteLine($"Letras usadas: {string.Join(", ", _motor.LetrasUsadas)}");

            if (_motor.MostrarPista)
            {
                Console.WriteLine($"Pista: la palabra empieza con '{_motor.PalabraSecreta[0]}'");
            }

            Console.Write("Palabra: ");

            foreach (char c in _motor.PalabraSecreta)
            {
                Console.Write(_motor.LetrasUsadas.Contains(c) ? c : '_');
            }

            Console.WriteLine();
        }

        public char PedirLetra()
        {
            Console.Write("\nIngresa una letra: ");
            string entrada = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(entrada))
            {
                Console.Write("Ingresa una letra válida: ");
                entrada = Console.ReadLine();
            }

            return entrada.ToLower()[0];
        }

        public void MostrarMensaje(string mensaje)
        {
            Console.WriteLine(mensaje);
        }

        public bool PreguntarOtraVez()
        {
            Console.Write("\n¿Jugar otra vez? (s/n): ");
            return Console.ReadLine()?.ToLower() == "s";
        }

        public static string PedirCategoria()
        {
            Console.WriteLine("=== CATEGORÍAS ===");
            Console.WriteLine("1. Arquitectura");
            Console.WriteLine("2. POO");
            Console.WriteLine("3. .NET");
            Console.Write("\nElige una categoría: ");

            string opcion = Console.ReadLine();

            return opcion switch
            {
                "1" => "arquitectura",
                "2" => "poo",
                "3" => "net",
                _ => "arquitectura"
            };
        }

        private void MostrarAhorcado()
        {
            string[] etapas = new string[]
            {
                " -----\n |   |\n     |\n     |\n     |\n     |\n=========",
                " -----\n |   |\n O   |\n     |\n     |\n     |\n=========",
                " -----\n |   |\n O   |\n |   |\n     |\n     |\n=========",
                " -----\n |   |\n O   |\n/|   |\n     |\n     |\n=========",
                " -----\n |   |\n O   |\n/|\\  |\n     |\n     |\n=========",
                " -----\n |   |\n O   |\n/|\\  |\n/    |\n     |\n=========",
                " -----\n |   |\n O   |\n/|\\  |\n/ \\  |\n     |\n========="
            };

            Console.WriteLine(etapas[6 - _motor.IntentosRestantes]);
        }
    }
}