namespace Ahorcado
{
    public class ConsolaUIViborita
    {
        private readonly IMotorViborita _motor;

        public ConsolaUIViborita(IMotorViborita motor)
        {
            _motor = motor;
        }

        public void MostrarTablero()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"=== VIBORITA === Puntos: {_motor.Puntos}");
            Console.WriteLine("+" + new string('-', _motor.Ancho) + "+");

            for (int y = 0; y < _motor.Alto; y++)
            {
                Console.Write("|");

                for (int x = 0; x < _motor.Ancho; x++)
                {
                    var posicion = (x, y);

                    if (_motor.Cuerpo[0] == posicion)
                        Console.Write("@");
                    else if (_motor.Cuerpo.Contains(posicion))
                        Console.Write("o");
                    else if (_motor.Comida == posicion)
                        Console.Write("*");
                    else
                        Console.Write(" ");
                }

                Console.WriteLine("|");
            }

            Console.WriteLine("+" + new string('-', _motor.Ancho) + "+");
            Console.WriteLine("Flechas: mover | Q: salir");
        }

        public ConsoleKey LeerTecla()
        {
            return Console.KeyAvailable
                ? Console.ReadKey(intercept: true).Key
                : ConsoleKey.NoName;
        }

        public void MostrarMensaje(string mensaje)
        {
            Console.WriteLine(mensaje);
        }
    }
}
