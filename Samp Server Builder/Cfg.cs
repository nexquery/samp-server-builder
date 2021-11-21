using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samp_Server_Builder
{
    public static class Cfg
    {
        public static string[] e_037_dl =
        {
            "echo Executing Server Config...",
            "lanmode 0",
            "rcon_password pw123",
            "maxplayers 50",
            "port 7777",
            "hostname {0}",
            "gamemode0 {1} 1",
            "filterscripts",
            "plugins",
            "announce 1",
            "query 1",
            "chatlogging 0",
            "weburl www.sa-mp.com",
            "maxnpc 0",
            "onfoot_rate 40",
            "incar_rate 40",
            "weapon_rate 40",
            "stream_distance 300.0",
            "stream_rate 1000",
            "logtimeformat [%H:%M:%S]",
            "language English",
            "sleep 1",
            "useartwork 1",
            "mtu 1400"
        };
    }
}
