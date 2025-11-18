using Dynamic914.Dependency;
using LabApi.Features.Wrappers;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Logger = LabApi.Features.Console.Logger;

namespace DynamicKeycards.Dynamic914
{
    internal class OutputDynamicKeycard : OutputEntry
    {
        private readonly string _name;

        private OutputDynamicKeycard(string name)
        {
            _name = name;
        }

        public override void GiveTo(Player player)
        {
            DynamicKeycards.Instance.GetKeycard(_name).Give(player, player.Nickname);
        }

        public override void Spawn(Vector3 position)
        {
            DynamicKeycards.Instance.GetKeycard(_name).Spawn(position, RandomName());
        }

        private static string RandomName()
        {
            var index = RandomGenerator.GetUInt16() % Player.List.Count;
            return Player.List.ElementAt(index).Nickname;
        }

        public static OutputEntry Create(JToken value)
        {
            if (value.Type != JTokenType.String)
            {
                Logger.Error($"Property \"value\" must be a string!");
                return null;
            }

            var strValue = (value as JValue).Value as string;

            var keycard = DynamicKeycards.Instance.GetKeycard(strValue);

            if (keycard is null)
            {
                Logger.Error($"Could not find a Dynamic Keycard with id \"{strValue}\"");
                return null;
            }

            return new OutputDynamicKeycard(strValue);
        }
    }
}