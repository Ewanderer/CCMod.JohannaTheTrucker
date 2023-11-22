using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;

namespace JohannaTheTrucker
{
    public partial class Manifest
    {
        public static ExternalPart? Ship_Bay_Part { get; private set; }
        public static ExternalPart? Ship_Cannon_Part { get; private set; }
        public static ExternalPart? Ship_CargoLeft_Part { get; private set; }
        public static ExternalPart? Ship_CargoRight_Part { get; private set; }
        public static ExternalPart? Ship_Cockpit_Part { get; private set; }
        public static ExternalPart? Ship_Hull_Part { get; private set; }
        public static ExternalShip? Ship_UrsaeMajoris_Main { get; private set; }
        public static ExternalPart? Ship_Scaffholding_Part { get; private set; }
        public static ExternalStarterShip? Ship_UrsaeMajoris_Starter { get; private set; }

        public void LoadManifest(IShipPartRegistry registry)
        {
            Ship_Bay_Part = new ExternalPart("JohannaTheTrucker.Parts.UrsaeMajorisBay",
                new Part()
                {
                    active = true,
                    type = PType.missiles
                },
                Ship_Bay_Sprite ?? throw new Exception());
            Ship_Cannon_Part = new ExternalPart("JohannaTheTrucker.Parts.UrsaeMajorisCannon",
              new Part()
              {
                  active = true,
                  type = PType.cannon
              },
              Ship_Cannon_Sprite ?? throw new Exception());
            Ship_CargoLeft_Part = new ExternalPart("JohannaTheTrucker.Parts.UrsaeMajorisCargoLeft",
            new Part()
            {
                active = true,
                type = PType.wing,
                damageModifier = PDamMod.armor
            },
            Ship_Cargo_Left_Sprite ?? throw new Exception());

            Ship_CargoRight_Part = new ExternalPart("JohannaTheTrucker.Parts.UrsaeMajorisCargoRight",
             new Part()
             {
                 active = true,
                 type = PType.wing,
                 damageModifier = PDamMod.armor
             },
            Ship_Cargo_Right_Sprite ?? throw new Exception());

            Ship_Cockpit_Part = new ExternalPart("JohannaTheTrucker.Parts.UrsaeMajorisCockpit",
                new Part()
                {
                    active = true,
                    type = PType.cockpit
                },
                Ship_Cockpit_Sprite ?? throw new Exception());

            Ship_Hull_Part = new ExternalPart("JohannaTheTrucker.Parts.UrsaeMajorisHull",
              new Part()
              {
                  active = true,
                  type = PType.wing
              },
              Ship_Hull_Sprite ?? throw new Exception());

            Ship_Scaffholding_Part = new ExternalPart("JohannaTheTrucker.Parts.UrsaeMajorisScaffholding",
          new Part()
          {
              active = true,
              type = PType.empty
          },
          Ship_Scaffholding_Sprite ?? throw new Exception());

            registry.RegisterPart(Ship_Bay_Part);
            registry.RegisterPart(Ship_Cockpit_Part);
            registry.RegisterPart(Ship_Hull_Part);
            registry.RegisterPart(Ship_Scaffholding_Part);
            registry.RegisterPart(Ship_Cannon_Part);
            registry.RegisterPart(Ship_CargoLeft_Part);
            registry.RegisterPart(Ship_CargoRight_Part);
        }

        public void LoadManifest(IShipRegistry shipRegistry)
        {
            Ship_UrsaeMajoris_Main = new ExternalShip("JohannaTheTrucker.Ships.UrsaeMajoris",
                new Ship()
                {
                    hull = 18,
                    hullMax = 18,
                    shieldMaxBase = 5,
                    baseDraw = 6
                },
                new ExternalPart[] { Ship_CargoLeft_Part ?? throw new Exception(), Ship_Cannon_Part ?? throw new Exception(), Ship_Cockpit_Part ?? throw new Exception(), Ship_Hull_Part ?? throw new Exception(), Ship_Bay_Part ?? throw new Exception(), Ship_CargoRight_Part ?? throw new Exception() },
                Ship_Chassis_Sprite);
            shipRegistry.RegisterShip(Ship_UrsaeMajoris_Main);
        }

        public void LoadManifest(IStartershipRegistry registry)
        {
            Ship_UrsaeMajoris_Starter = new ExternalStarterShip("JohannaTheTrucker.Starterships.UrsaeMajoris",
                Ship_UrsaeMajoris_Main ?? throw new Exception(),
                new ExternalCard[] { BasicDefensivesCard ?? throw new Exception(), BasicFileSearchCard ?? throw new Exception(), BasicBlastCard ?? throw new Exception(), BasicBlastCard ?? throw new Exception()},
                new ExternalArtifact[] { ExtraCargoPodArtifact ?? throw new Exception() },
                new Type[] { typeof(ShieldPrep) });

            Ship_UrsaeMajoris_Starter.AddLocalisation("Ursae Majoris", "An armored cargo ship, infamous among pirates who flee on sight.");

            registry.RegisterStartership(Ship_UrsaeMajoris_Starter);


        }
    }
}
