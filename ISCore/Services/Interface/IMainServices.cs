using ISCore.Services.Places;

namespace ISCore.Services.Interface
{
    public interface IMainServices
    {
        Srv_Countries _SrvCountries { get;}
        Srv_Cities _SrvCities { get;}
        Srv_Regions _SrvRegions { get;}

    }
}
