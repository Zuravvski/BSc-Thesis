using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Infrastructure.Commands;
using Infrastructure.Repositories;
using Newtonsoft.Json;

namespace Infrastructure.Services.Camera
{
    public class CameraService : ICameraService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICameraCommandDispatcher _commandDispatcher;
        private readonly Process _cameraProcess;

        public bool Running { get; protected set; }

        public CameraService(IClientRepository clientRepository, ICameraCommandDispatcher commandDispatcher)
        {
            _clientRepository = clientRepository;
            _commandDispatcher = commandDispatcher;
            _cameraProcess = new Process
            {
                StartInfo = new ProcessStartInfo("CameraService.exe")
                {
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = false,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden
                },
                EnableRaisingEvents = true
            };
        }

        public void Start()
        {
            if (Running) return;

            _cameraProcess.Exited += (sender, args) => _cameraProcess.Dispose();

            try
            {
                Running = _cameraProcess.Start();

                if (Running)
                {
                    ReadAsync();
                }
            }
            catch
            {
                Debug.WriteLine("Could not start the camera");
            }
        }

        private async void ReadAsync()
        {
            var stdout = _cameraProcess.StandardOutput;
            while (Running)
            {
                try
                {
                    var buffer = new char[1024];
                    var bytesRead = await stdout.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        _cameraProcess.Close();
                        Running = false;
                        continue;
                    }
                    var command = JsonConvert.DeserializeObject<ICameraCommand>(Convert.ToString(buffer));
                    await _commandDispatcher.DispatchAsync(command);
                }
                catch
                {
                    Debug.WriteLine("Could not read the data from camera");
                }
            }
        }

        public void Stop()
        {
            if (!Running) return;

            _cameraProcess.Close();
            Running = false;
        }

        public async Task RequestAsync(ICameraCommand command)
        {
            try
            {
                var json = JsonConvert.SerializeObject(command);
                await _cameraProcess.StandardInput.WriteAsync(json);
            }
            catch
            {
                Debug.WriteLine($"Failed to send over a {command.GetType()} command");
            }
        }
    }
}
