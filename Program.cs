bool jugarOtraVez;

do
{
    Console.Clear();

    string categoria = Ahorcado.ConsolaUI.PedirCategoria();

    var repositorio = new Ahorcado.PalabrasEnMemoria(categoria);
    var motor = new Ahorcado.MotorAhorcado(repositorio);
    var ui = new Ahorcado.ConsolaUI(motor);

    Console.WriteLine("\n=== AHORCADO ===");

    while (!motor.Ganado() && !motor.Perdido())
    {
        ui.MostrarTablero();

        char letra = ui.PedirLetra();

        if (motor.LetraYaUsada(letra))
        {
            ui.MostrarMensaje("Ya usaste esa letra.");
            Console.WriteLine("Presiona una tecla para continuar...");
            Console.ReadKey();
            continue;
        }

        motor.RegistrarLetra(letra);
    }

    ui.MostrarTablero();

    if (motor.Ganado())
    {
        ui.MostrarMensaje($"\n¡Ganaste! La palabra era: {motor.PalabraSecreta}");
    }
    else
    {
        ui.MostrarMensaje($"\nPerdiste. La palabra era: {motor.PalabraSecreta}");
    }

    jugarOtraVez = ui.PreguntarOtraVez();

} while (jugarOtraVez);