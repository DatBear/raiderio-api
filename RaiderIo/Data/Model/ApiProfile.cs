using System;
using Newtonsoft.Json;

namespace RaiderIo.Data.Model {
    public class Profile {
        public string name { get; set; }
        public string race { get; set; }
        public string @class { get; set; }
        public string active_spec_name { get; set; }
        public string active_spec_role { get; set; }
        public string gender { get; set; }
        public string faction { get; set; }
        public string region { get; set; }
        public string realm { get; set; }
        public string profile_url { get; set; }
        public Gear gear { get; set; }
        public RaidProgression raid_progression { get; set; }
        public MythicPlusRanks mythic_plus_ranks { get; set; }
        public MythicPlusScores mythic_plus_scores { get; set; }
        public MythicPlusRanks previous_mythic_plus_ranks { get; set; }
        public MythicPlusScores previous_mythic_plus_scores { get; set; }
        public MythicPlusRun[] mythic_plus_recent_runs { get; set; }
        public MythicPlusRun[] mythic_plus_best_runs { get; set; }
        public MythicPlusRun[] mythic_plus_highest_level_runs { get; set; }
        public MythicPlusRun[] mythic_plus_weekly_highest_level_runs { get; set; }
    }

    public class Gear {
        public int item_level_equipped { get; set; }
        public int item_level_total { get; set; }
        public int artifact_traits { get; set; }
    }

    public class RaidProgression {
        [JsonProperty("antorus-the-burning-throne")]
        public Raid antorustheburningthrone { get; set; }
        [JsonProperty("tomb-of-sargeras")]
        public Raid tombofsargeras { get; set; }
        [JsonProperty("the-nighthold")]
        public Raid thenighthold { get; set; }
        [JsonProperty("the-emerald-nightmare")]
        public Raid theemeraldnightmare { get; set; }
        [JsonProperty("trial-of-valor")]
        public Raid trialofvalor { get; set; }
    }

    public class Raid {
        public string summary { get; set; }
        public int total_bosses { get; set; }
        public int normal_bosses_killed { get; set; }
        public int heroic_bosses_killed { get; set; }
        public int mythic_bosses_killed { get; set; }
    }

    public class MythicPlusRanks {
        public OverallRank overall { get; set; }
        public TankRank tank { get; set; }
        public HealerRank healer { get; set; }
        public DpsRank dps { get; set; }
        public ClassRank _class { get; set; }
        public TankClassRank class_tank { get; set; }
        public HealerClassRank class_healer { get; set; }
        public DpsClassRank class_dps { get; set; }
    }

    public interface IRank {
        RankType RankType { get; set; }
        int world { get; set; }
        int region { get; set; }
        int realm { get; set; }
    }

    public enum RankType {
        Overall,
        Tank,
        Healer,
        Dps,
        ClassRank,
        TankClass,
        HealerClass,
        DpsClass
    }

    public class OverallRank : IRank {
        public RankType RankType { get; set; } = RankType.Overall;
        public int world { get; set; }
        public int region { get; set; }
        public int realm { get; set; }
    }

    public class TankRank : IRank {
        public RankType RankType { get; set; } = RankType.Tank;
        public int world { get; set; }
        public int region { get; set; }
        public int realm { get; set; }
    }

    public class HealerRank : IRank {
        public RankType RankType { get; set; } = RankType.Healer;
        public int world { get; set; }
        public int region { get; set; }
        public int realm { get; set; }
    }

    public class DpsRank : IRank {
        public RankType RankType { get; set; } = RankType.Dps;
        public int world { get; set; }
        public int region { get; set; }
        public int realm { get; set; }
    }

    public class ClassRank : IRank {
        public RankType RankType { get; set; } = RankType.ClassRank;
        public int world { get; set; }
        public int region { get; set; }
        public int realm { get; set; }
    }

    public class TankClassRank : IRank {
        public RankType RankType { get; set; } = RankType.TankClass;
        public int world { get; set; }
        public int region { get; set; }
        public int realm { get; set; }
    }

    public class HealerClassRank : IRank {
        public RankType RankType { get; set; } = RankType.HealerClass;
        public int world { get; set; }
        public int region { get; set; }
        public int realm { get; set; }
    }

    public class DpsClassRank : IRank {
        public RankType RankType { get; set; } = RankType.DpsClass;
        public int world { get; set; }
        public int region { get; set; }
        public int realm { get; set; }
    }

    public class MythicPlusScores {
        public decimal all { get; set; }
        public decimal dps { get; set; }
        public decimal healer { get; set; }
        public decimal tank { get; set; }
    }

    public class MythicPlusRun {
        public string dungeon { get; set; }
        public string short_name { get; set; }
        public int mythic_level { get; set; }
        //public DateTime? completed_at { get; set; }
        public int clear_time_ms { get; set; }
        public int num_keystone_upgrades { get; set; }
        public decimal score { get; set; }
        public string url { get; set; }
    }
}