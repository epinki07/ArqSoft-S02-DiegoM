using Ahorcado;

Console.CursorVisible = false;

var motor = new MotorViborita(ancho: 30, alto: 15);
var ui = new ConsolaUIViborita(motor);

while (!motor.Perdido())
{
    ui.MostrarTablero();

    ConsoleKey tecla = ui.LeerTecla();

    if (tecla == ConsoleKey.Q)
        break;

    motor.CambiarDireccion(tecla);
    motor.Avanzar();

    Thread.Sleep(120);
}

ui.MostrarTablero();
Console.CursorVisible = true;

ui.MostrarMensaje(motor.Perdido()
    ? "Juego terminado. Presiona una tecla para salir."
    : "Saliste del juego. Presiona una tecla para cerrar.");

Console.ReadKey(intercept: true);
