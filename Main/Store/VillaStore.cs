using Main.Models.Dto;

namespace Main.Store
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new()
        {
            new VillaDTO{ Id = 1, Name = "AAA", Occupancy = 4, Sqft = 100},
            new VillaDTO{ Id = 2, Name = "BBB", Occupancy = 2, Sqft = 200}
        };
    }
}
