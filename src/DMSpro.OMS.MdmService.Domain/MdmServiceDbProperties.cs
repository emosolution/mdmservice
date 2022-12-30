namespace DMSpro.OMS.MdmService;

public static class MdmServiceDbProperties
{
    public static string DbTablePrefix { get; set; } = "";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "MdmService";
}
