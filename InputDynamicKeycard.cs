using Dynamic914.Dependency;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Logger = LabApi.Features.Console.Logger;

namespace DynamicKeycards.Dynamic914
{
    internal class InputDynamicKeycard : Input
    {
        private string _name;

        private InputDynamicKeycard(string name, Output rough, Output coarse, Output oneToOne, Output fine, Output veryFine)
            : base(rough, coarse, oneToOne, fine, veryFine)
        {
            _name = name;
        }

        public override int Priority => 1;

        public override bool RecipeIsFor(Item item)
        {
            bool isFor = Keycard.ItemIsDynamicKeycard(item, out var keycard);

            return (isFor && keycard.ID == _name);
        }

        public override bool RecipeIsFor(Pickup pickup)
        {
            bool isFor = Keycard.PickupIsDynamicKeycard(pickup, out var keycard);

            return (isFor && keycard.ID == _name);
        }

        public static Input Create(JToken value, Output rough, Output coarse, Output oneToOne, Output fine, Output veryFine)
        {
            if (value.Type != JTokenType.String)
            {
                Logger.Error("Property \"value\" must be a string!");
                return null;
            }

            var strValue = (value as JValue).Value as string;

            var keycard = DynamicKeycards.Instance.GetKeycard(strValue);

            if (keycard is null)
            {
                Logger.Error($"Could not find a Dynamic Keycard with id \"{strValue}\"");
                return null;
            }

            return new InputDynamicKeycard(strValue, rough, coarse, oneToOne, fine, veryFine);
        }

        protected override string DebugStringRecipeIsFor()
        {
            return _name;
        }
    }
}