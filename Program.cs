using Ahorcado;

bool salir = false;

while (!salir)
{
    Console.CursorVisible = true;
    Console.Clear();
    Console.WriteLine("=== MENU DE JUEGOS ===");
    Console.WriteLine("1. Ahorcado");
    Console.WriteLine("2. Viborita");
    Console.WriteLine("3. Salir");
    Console.Write("\nElige una opcion: ");

    switch (Console.ReadLine())
    {
        case "1":
            JugarAhorcado();
            break;
        case "2":
            JugarViborita();
            break;
        case "3":
            salir = true;
            break;
        default:
            Console.WriteLine("Opcion no valida. Presiona una tecla para continuar.");
            Console.ReadKey(intercept: true);
            break;
    }
}

static void JugarAhorcado()
{
    bool jugarOtraVez;

    do
    {
        Console.Clear();

        string categoria = ConsolaUI.PedirCategoria();
        var repositorio = new PalabrasEnMemoria(categoria);
        var motor = new MotorAhorcado(repositorio);
        var ui = new ConsolaUI(motor);

        Console.WriteLine("\n=== AHORCADO ===");

        while (!motor.Ganado() && !motor.Perdido())
        {
            ui.MostrarTablero();

            char letra = ui.PedirLetra();

            if (motor.LetraYaUsada(letra))
            {
                ui.MostrarMensaje("Ya usaste esa letra.");
                Console.WriteLine("Presiona una tecla para continuar...");
                Console.ReadKey(intercept: true);
                continue;
            }

            motor.RegistrarLetra(letra);
        }

        ui.MostrarTablero();

        ui.MostrarMensaje(motor.Ganado()
            ? $"\nGanaste. La palabra era: {motor.PalabraSecreta}"
            : $"\nPerdiste. La palabra era: {motor.PalabraSecreta}");

        jugarOtraVez = ui.PreguntarOtraVez();
    }
    while (jugarOtraVez);
}

static void JugarViborita()
{
    Console.CursorVisible = false;

    var motor = new MotorViborita(ancho: 30, alto: 15);
    var ui = new ConsolaUIViborita(motor);

    while (!motor.Perdido() && !motor.Ganado())
    {
        ui.MostrarTablero();

        ConsoleKey tecla = ui.LeerTecla();

        if (tecla == ConsoleKey.Q)
            break;

        motor.CambiarDireccion(tecla);
        motor.Avanzar();

        Thread.Sleep(110);
    }

    ui.MostrarTablero();
    Console.CursorVisible = true;

    if (motor.Ganado())
        ui.MostrarMensaje("Ganaste. Llenaste todo el tablero.");
    else if (motor.Perdido())
        ui.MostrarMensaje("Juego terminado.");
    else
        ui.MostrarMensaje("Saliste del juego.");

    Console.WriteLine("Presiona una tecla para volver al menu.");
    Console.ReadKey(intercept: true);
}
