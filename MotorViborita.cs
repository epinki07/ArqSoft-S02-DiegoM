namespace Ahorcado
{
    public class MotorViborita : IMotorViborita
    {
        private readonly Random _random = new();
        private readonly List<(int X, int Y)> _cuerpo = new();
        private Direccion _direccion = Direccion.Derecha;
        private bool _perdido;

        public int Ancho { get; }
        public int Alto { get; }
        public int Puntos { get; private set; }
        public IReadOnlyList<(int X, int Y)> Cuerpo => _cuerpo;
        public (int X, int Y) Comida { get; private set; }

        public MotorViborita(int ancho, int alto)
        {
            Ancho = ancho;
            Alto = alto;

            int centroX = ancho / 2;
            int centroY = alto / 2;

            _cuerpo.Add((centroX, centroY));
            _cuerpo.Add((centroX - 1, centroY));
            _cuerpo.Add((centroX - 2, centroY));

            GenerarComida();
        }

        public void CambiarDireccion(ConsoleKey tecla)
        {
            Direccion nuevaDireccion = tecla switch
            {
                ConsoleKey.UpArrow or ConsoleKey.W => Direccion.Arriba,
                ConsoleKey.DownArrow or ConsoleKey.S => Direccion.Abajo,
                ConsoleKey.LeftArrow or ConsoleKey.A => Direccion.Izquierda,
                ConsoleKey.RightArrow or ConsoleKey.D => Direccion.Derecha,
                _ => _direccion
            };

            if (!EsDireccionOpuesta(nuevaDireccion))
                _direccion = nuevaDireccion;
        }

        public void Avanzar()
        {
            if (_perdido)
                return;

            var cabeza = _cuerpo[0];
            var nuevaCabeza = _direccion switch
            {
                Direccion.Arriba => (cabeza.X, cabeza.Y - 1),
                Direccion.Abajo => (cabeza.X, cabeza.Y + 1),
                Direccion.Izquierda => (cabeza.X - 1, cabeza.Y),
                _ => (cabeza.X + 1, cabeza.Y)
            };

            bool come = nuevaCabeza == Comida;
            int segmentosPermitidos = come ? _cuerpo.Count : _cuerpo.Count - 1;

            if (ChocaConPared(nuevaCabeza) || _cuerpo.Take(segmentosPermitidos).Contains(nuevaCabeza))
            {
                _perdido = true;
                return;
            }

            _cuerpo.Insert(0, nuevaCabeza);

            if (come)
            {
                Puntos += 10;
                GenerarComida();
            }
            else
            {
                _cuerpo.RemoveAt(_cuerpo.Count - 1);
            }
        }

        public bool Ganado()
        {
            return _cuerpo.Count == Ancho * Alto;
        }

        public bool Perdido()
        {
            return _perdido;
        }

        private bool ChocaConPared((int X, int Y) posicion)
        {
            return posicion.X < 0 || posicion.X >= Ancho || posicion.Y < 0 || posicion.Y >= Alto;
        }

        private bool EsDireccionOpuesta(Direccion nuevaDireccion)
        {
            return (_direccion == Direccion.Arriba && nuevaDireccion == Direccion.Abajo)
                || (_direccion == Direccion.Abajo && nuevaDireccion == Direccion.Arriba)
                || (_direccion == Direccion.Izquierda && nuevaDireccion == Direccion.Derecha)
                || (_direccion == Direccion.Derecha && nuevaDireccion == Direccion.Izquierda);
        }

        private void GenerarComida()
        {
            do
            {
                Comida = (_random.Next(Ancho), _random.Next(Alto));
            }
            while (_cuerpo.Contains(Comida));
        }

        private enum Direccion
        {
            Arriba,
            Abajo,
            Izquierda,
            Derecha
        }
    }
}
