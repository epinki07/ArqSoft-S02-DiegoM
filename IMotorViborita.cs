namespace Ahorcado
{
    public interface IMotorViborita : IMotorJuego
    {
        int Ancho { get; }
        int Alto { get; }
        int Puntos { get; }
        IReadOnlyList<(int X, int Y)> Cuerpo { get; }
        (int X, int Y) Comida { get; }

        void CambiarDireccion(ConsoleKey tecla);
        void Avanzar();
    }
}
