using Dynamic914.Dependency;
using LabApi.Events.Handlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using Scp914;
using Scp914.Processors;

namespace DynamicKeycards.Dynamic914
{
    public class DynamicKeycardsForDynamic914 : Plugin
    {
        public override string Name => "Dynamic Keycards for Dynamic 914";

        public override string Description => "Implements Dynamic Keycards support for Dynamic 914";

        public override string Author => "TeamEXAngus";

        public override Version Version => new Version(1, 0, 0);

        public override Version RequiredApiVersion => new Version(LabApiProperties.CompiledVersion);

        public override LoadPriority Priority => LoadPriority.Medium;

        public override void Disable()
        {
        }

        public override void Enable()
        {
            Input.RegisterInput("dynamickeycard", InputDynamicKeycard.Create);
            Output.RegisterOutput("dynamickeycard", OutputDynamicKeycard.Create);
        }
    }
}