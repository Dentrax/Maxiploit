#region License
// ====================================================
// Maxiploit Copyright(C) 2017 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.Collections.Generic;
using Maxiploit.Modules.VSRO.Data;
using Maxiploit.Modules.VSRO.Network;
using Maxiploit.Core.IO;
using Maxiploit.Common.Log;

namespace Maxiploit.Modules.VSRO {
    public sealed class VSROSettings {

        //abcd

        //Test
        //Başka bir değişiklik

            //adssad

        //ATOM_PARÇALA.ex

        private const string SETTINGS_PATH = "settings.cfg";

        public const string FILTER = "[Maxiploit]";
        public readonly Version VERSION = new Version(1, 1, 1, 1);

        public readonly string DATE = "[" + DateTime.UtcNow + "]";

        public const string FILES = "VSRO";

        #region DB

        public string DB_ACCOUNT = "SRO_VT_ACCOUNT";
        public string DB_SHARD = "SRO_VT_SHARD";
        public string DB_LOG = "SRO_VT_LOG";

        #endregion

        #region CONNECTION

        public int GATEWAY_COUNT = 25;
        public int AGENT_COUNT = 25;

        public const double MAX_BYTES_PER_SEC_GATEWAY = 100.00;
        public const double MAX_BYTES_PER_SEC_AGENT = 1000.00;

        public const string BIND_IP = "0.0.0.0";

        public int GATEWAY_LISTEN_PORT = 1337;
        public int AGENT_LISTEN_PORT = 1338;
        public int AGENT2_LISTEN_PORT = 1338;

        public string REMOTE_GATEWAY_IP = "127.0.0.1";
        public string REMOTE_AGENT_IP = "127.0.0.1";
        public string REMOTE_AGENT_2_IP = "127.0.0.1";

        public int FAKE_GATEWAY_PORT = 15779;
        public int FAKE_AGENT_PORT = 15884;
        public int FAKE_AGENT2_PORT = 15886;

        public string PROXY = string.Empty;


        public int MaxConnServer { get; private set; }

        public int MaxConnPerIp { get; private set; }

        public int ModuleConnTimeout { get; private set; }

        public int SessActivityTimeout { get; private set; }

        public int SessProcThreads { get; private set; }

        public int ListenerBacklog { get; private set; }

        public int SessionBufferCount { get; private set; }

        #endregion

        #region LOG

        public bool ENABLE_LOGGING = true;

        public string LogFolder { get; private set; }

        public int LogTime { get; private set; }

        public bool ENABLE_EXCEPTION_LOG = true;

        public bool ENABLE_ERROR_LOG = true;

        public bool ENABLE_INFO_LOG = true;

        public bool ENABLE_PLAYERS_LOG = false;


        #endregion

        #region OPCODES

        public Dictionary<ushort, string> OPCODES = new Dictionary<ushort, string>();
        public Dictionary<ushort, string> BAD_OPCODES = new Dictionary<ushort, string>();

        public List<string> LAST_OPCODES = new List<string>();

        #endregion

        #region ANTIS

        public string FLOOD_METHOD = string.Empty;
        public string EXPLOIT_METHOD = string.Empty;
        public string PACKET_METHOD = string.Empty;
        public string UNKNOWN_METHOD = string.Empty;

        #endregion


        public ServerType ServerType { get; private set; }

        public List<RedirectionRule> RedirectionRules { get; private set; }

       
        public int UPDATED = 30;

        public int exploit_count = 0;

        public bool flood_fix = false;

        public bool hwid_system = false;

        public string host = string.Empty;
        public string user = string.Empty;
        public string pass = string.Empty;
        public string shard = string.Empty;
        public bool DB = false;
       

        public bool block_status = false;
        public string ServerName = Environment.UserName;
        public int shard_players = 0;
        public int FAKE_PLAYERS = 0;
        public int MAX_PLAYERS = 1000;
        public int ShardID = 64;

        public bool CURRENT_REGION = false;
        public bool STORE_HWID = false;


        #region LIMITS

        public int IPLIMIT = 0;
        public int IPGLOBAL = 0;
        public int CAFELIMIT = 0;
        public int PCLIMIT = 0;
        public int FLOOD_LIMIT = 30;
        public int PLAYERLIMIT = 250;
        public int PLUSLIMIT = 0;

        #endregion

        #region LISTS

        public List<string> ip_list = new List<string>();
        public List<string> flood_list = new List<string>();
        public List<string> cafe_list = new List<string>();
        public List<string> hwid_list = new List<string>();
        public Dictionary<string, string> hwid_user = new Dictionary<string, string>();
        public List<string> LIMIT_BYPASS = new List<string>();
        public List<string> USER_ID = new List<string>();
        public List<string> ban_list = new List<string>();
        public List<string> unknown_list = new List<string>();

        public List<int> fortressRegions = new List<int>();
        public List<short> Regions = new List<short>();
        public List<uint> fortressTele = new List<uint>();
        public List<uint> Jobcavetele = new List<uint>();
        public List<short> tradeRegions = new List<short>();
        public List<int> FortressSkills = new List<int>();
        public List<int> BlockedSkills = new List<int>();
        public List<string> badwords = new List<string>();

        public List<string> EVENT_CHARS = new List<string>();

        #endregion

        #region GATEWAY_SETTINGS

        #region CAPTCHA

        public bool ENABLE_CAPTCHA = false;
        public string CAPTCHA_VALUE = string.Empty;

        #endregion

        #endregion

        #region AGENT_SETTINGS

        #region SILK

        public bool ENABLE_AUTO_SILK = false;
        public bool ENABLE_AUTO_SILK_NOTICE = false;

        public int AUTO_SILK_MIN_LEVEL = 0;
        public int AUTO_SILK_AMOUNT = 1;
        public int AUTO_SILK_DELAY = 3600000;
       
        public bool AUTO_SILK_IF_AFK = false;

        #endregion

        #region WELCOME

        public bool ENABLE_WELCOME_MESSAGE = false;
       
        public string MESSAGE_HANDLE = string.Empty;

        #endregion

        #region STALL

        public bool ENABLE_STALL_DELAY = false;
        public int STALL_DELAY = 10;
        public int STALL_LEVEL_MIN = 0;

        #endregion

        #region EXCHANGE

        public bool ENABLE_EXCHANGE_DELAY = false;
        public int EXCHANGE_DELAY = 10;
        public int EXCHANGE_LEVEL_MIN = 0;

        #endregion

        #region REVERSE

        public bool ENABLE_REVERSE_DELAY = false;
        public int REVERSE_DELAY = 0;

        #endregion

        #region LOGOUT

        public bool ENABLE_LOGOUT_DELAY = false;
        public int LOGOUT_DELAY = 0;

        #endregion

        #region RESTART

        public bool ENABLE_RESTART_BUTTON = false;
        public bool ENABLE_RESTART_DELAY = false;
        public int RESTART_DELAY = 0;

        #endregion

        #region AVATAR

        public bool ENABLE_AVATAR_BLUES = false;

        #endregion

        #region ALCHEMY

        public bool ENABLE_PLUS_SUCCESSFULL_NOTICE = false;
        public bool ENABLE_PLUS_UNSUCCESSFULL_NOTICE = false;

        public int REQUIRED_PLUS_FOR_SUCCESSFULL_NOTICE = 0;
        public int REQUIRED_PLUS_FOR_UNSUCCESSFULL_NOTICE = 0;

        #endregion

        #region ZERK

        public bool ENABLE_ZERK_DELAY = false;
        public int ZERK_DELAY = 0;

        #endregion

        #region GLOBAL

        public bool ENABLE_GLOBAL_DELAY = false;

        public int GLOBAL_DELAY = 0;

        public int GLOBAL_LEVEL_MIN = 0;

        #endregion

        #region PVP

        public bool PVP_DUEL = false;
        public string PVP_WIN = string.Empty;
        public string PVP_LOOSE = string.Empty;
        public bool PVP_RANKING = false;
        public int PVP_TITLE = 0;
        public bool PVP_DISABLE_PARTY = false;
        public bool PVP_DISABLE_OUTSIDERS = false;
        public bool PVP_DISABLE_ZERK = false;

        #endregion

        #region GUILD

        public int GUILD_LIMIT = 0;

        #endregion

        #region UNION

        public int UNION_LIMIT = 0;

        #endregion

        #region ARENA

        public bool CALLED_ARENA = false;

        public bool ENABLE_ARENA = true;
        public int ARENA_LEVEL_MIN = 0;
        public int ARENA_LEVEL_MAX = 0;
       

        #endregion

        #region ACADEMY

        public bool ENABLE_ACADEMY = true;
        public bool ENABLE_ACADEMY_INVITE = true;

        #endregion

        #region FORTRESS

        public bool ENABLE_FORTRESS = false;

        public bool ENABLE_REVERSE_DURING_FORTRESS = false;
        public bool ENABLE_RES_DURING_FORTRESS = false;
        public bool ENABLE_EXCHANGE_DURING_FORTRESS = false;
        public bool ENABLE_TRACE_DURING_FORTRESS = false;
        public bool ENABLE_PET_DURING_FORTRESS = false;

        #endregion

        #region JOB

        public bool ENABLE_JOB = false;

        public bool ENABLE_REVERSE_DURING_JOB = false;
        public bool ENABLE_RES_DURING_JOB = false;
        public bool ENABLE_EXCHANGE_DURING_JOB = false;
        public bool ENABLE_TRACE_DURING_JOB = false;
        public bool ENABLE_PET_DURING_JOB = false;

        public bool ENABLE_JOB_CHEAT = false;

        public int THIEF_MUST_SELL = 0;

        #endregion

        #region CTF

        public bool CALLED_CTF = false;
       
        public bool ENABLE_CTF = false;

        public int CTF_LEVEL_MIN = 0;
        public int CTF_LEVEL_MAX = 0;

        #endregion

        #region DC

        public bool ENABLE_DC_NOTICE_BEFORE_GUARD_BASED_DC = false;

        #endregion

        #region BOT

        public List<string> BOT_LIST = new List<string>();
        public bool ENABLE_BOT_ALLOW = true;
        public bool ENABLE_BOT_ALCHEMY_ELIXIR = true;
        public bool ENABLE_BOT_ALCHEMY_STONE = true;
        public bool ENABLE_BOT_AVATAR_BLUES = true;
        public bool ENABLE_BOT_CREATE_PARTY = true;
        public bool ENABLE_BOT_INVITE_PARTY = true;
        public bool ENABLE_BOT_EXCHANGE = true;
        public bool ENABLE_BOT_STALL = true;
        public bool ENABLE_BOT_ARENA = true;
        public bool ENABLE_BOT_CTF = true;
        public bool ENABLE_BOT_FORTRESS = true;
        public bool ENABLE_BOT_DETECTION = false;
        public bool ENABLE_BOT_WARNING = false;
        public bool ENABLE_BOT_PVP = true;
        public bool ENABLE_BOT_TRACE = true;

        #endregion

        #region LOCKERS

        //Disable exhange, stall, no drops, no equip, no pick-up
        public bool ENABLE_LOCK_HARD_CHAR = false;

        #endregion

        #region GM

        public List<string> GM_ACCOUNT = new List<string>();
        public List<string> PRIV_IP = new List<string>();
        public bool GM_LOGIN = false;
        public bool START_VISIBLE = false;
        public bool GM_WHITE = true;

        #endregion

        #endregion

        #region MESSAGES

        public string MESSAGE_WELCOME_NOTICE = "Welcome ! {player_name}";

        public string MESSAGE_SILK_NOTICE = "SilkSystem : You have received 1 silk(s), for playing on the server!";
        public string MESSAGE_GM_NOTICE = "You are not allowed to use GM commands";

        public string MESSAGE_ACADEMY_CREATION = "Academy create: This system has been disabled!";
        public string MESSAGE_ACADEMY_INVITE = "Academy invite: This system has been disabled!";

        public string MESSAGE_BOT_NOTICE_FORTRESS = "Third-part programs aren't allowed to enter fortress!";
        public string MESSAGE_BOT_NOTICE_CTF = "Third-part programs aren't allowed to join CTF!";
        public string MESSAGE_BOT_NOTICE_ARENA = "Third-part programs aren't allowed to join Arena!";
        public string MESSAGE_BOT_NOTICE_FUSE = "Third-part programs aren't allowed to fuse items!";
        public string MESSAGE_BOT_NOTICE_ALCHEMY = "Third-part programs aren't allowed to fuse items!";
        public string MESSAGE_BOT_NOTICE_DISCONNECT = "Third-part programs aren't allowed to enter this server!";
        public string MESSAGE_BOT_NOTICE_AVATAR_BLUES = "Third-part programs aren't allowed to change avatar blues!";
        public string MESSAGE_BOT_NOTICE_STALL_CREATE = "Third-part programs aren't allowed to create a stall!";
        public string MESSAGE_BOT_NOTICE_PARTY_CREATE = "Third-part programs aren't allowed to create party matches!";
        public string MESSAGE_BOT_NOTICE_PARTY_INVITE = "Third-part programs aren't allowed to invite to party matches!";
        public string MESSAGE_BOT_NOTICE_EXCHANGE = "Third-part programs aren't allowed to exchange others!";
        public string MESSAGE_BOT_NOTICE_PVP = "Third-part programs aren't allowed to use PVP cape!";
        public string MESSAGE_BOT_NOTICE_TRACE = "Third-part programs aren't allowed to trace!";

        public string MESSAGE_CHEAT_NOTICE = "You are now being monitored! Don't try to cheat!";
        public string MESSAGE_CHEAT_NOTICE2 = "Cannot terminate vehicles and transports near towns. You can drop goods when your 300m out of town. PS: Click on the ground not the sky!";
        public string MESSAGE_CHEAT_NOTICE3 = "Picking items during job inside towns isnt allowed. PS: Click on the ground not the sky!";

        public string MESSAGE_SKILL_NOTICE = "You cannot use this skill inside fortress arena.";
        public string MESSAGE_SKILL_NOTICE2 = "You cannot trace inside the fortress arena.";
        public string MESSAGE_SKILL_NOTICE3 = "Rescurrent scrolls are disabled in fortress war!";

        public string MESSAGE_LOCAL_CHAT = "Do you kiss your mother with that mouth?";
        public string MESSAGE_PRIVATE_CHAT = "Do you kiss your mother with that mouth?";
        public string MESSAGE_PARTY_CHAT = "Do you kiss your mother with that mouth?";
        public string MESSAGE_PARTY_MATCH = "Cowabunga, dude!";
        public string MESSAGE_PRIVATE_MESSAGE = "Do you kiss your mother with that mouth?";
        public string MESSAGE_GLOBAL_MESSAGE = "You may not write this word, sorry!";
        public string MESSAGE_STALL_FILTER = "You may not write this word, sorry!";

        public string MESSAGE_CTF_LEVEL_MAX = "You are too low level, you need atleast level (1) to enter CTF!";
        public string MESSAGE_CTF_LEVEL_MIN = "You are too low level, you need atleast level (1) to enter CTF!";
        public string MESSAGE_ARENA_LEVEL_MIN = "You are too low level, you need atleast level (1) to enter Arena!";
        public string MESSAGE_ARENA_LEVEL_MAX = "You are too high level, you need atleast level (1) to enter Arena!";
        public string MESSAGE_GLOBAL_LEVEL = "You are too low level, you need atleast level (1) to use this item.";
        public string MESSAGE_STALL_LEVEL = "You are too low level, you need atleast level ({level}) to open a stall.";
        public string MESSAGE_UNION_LIMIT = "Union limit: Please teleport before inviting more unions!";
        public string MESSAGE_UNION_LIMIT2 = "Union limit: You have reached the max concurrent unions!";
        public string MESSAGE_GUILD_LIMIT = "Guild limit: Please teleport before inviting more members!";
        public string MESSAGE_GUILD_LIMIT2 = "Guild Limit: You have reached the max concurrent members!";
        public string MESSAGE_PLUS_LIMIT = "Plus limit: This item has reached it's maximum plus.";


        public string MESSAGE_EXCHANGE_DELAY = "Exchange delay: You must wait {time} more seconds.";
        public string MESSAGE_STALL_DELAY = "Stall delay: You must wait {time} more seconds.";
        public string MESSAGE_GLOBAL_DELAY = "Global delay: You must wait {time} more seconds.";
        public string MESSAGE_REVERSE_DELAY = "Reverse delay: You must wait {time} more seconds.";
        public string MESSAGE_LOGOUT_DELAY = "Logout delay: You must wait {time} more seconds.";
        public string MESSAGE_RESTART_DELAY = "Restart delay: You must wait {time} more seconds.";
        public string MESSAGE_ZERK_DELAY = "Zerk delay: You must wait {time} more seconds.";
        public string MESSAGE_STALL_EXPLOIT = "Stall exploit: Please teleport before opening stall.";

        public string MESSAGE_JOB_NOTICE_REVERSE = "Reverse scrolls are disabled under job state!";
        public string MESSAGE_JOB_NOTICE_TRACE = "You cannot trace during job mode, sorry!";
        public string MESSAGE_JOB_NOTICE_RESCURRENT = "Rescurrent scrolls are disabled under job state!";
        public string MESSAGE_JOB_NOTICE_EXCHANGE = "Exchange: Disabled under job state!";

        public string MESSAGE_RESTART_BUTTON = "The restart function is disabled, use exit!";
        public string MESSAGE_AVATAR_BLUES = "You cannot grant blues on avatars, sorry!";
        public string MESSAGE_AVATAR_EXPLOIT = "You cannot grant these blues on avatars!";
        public string MESSAGE_INVISIBLE_EXPLOIT = "This exploit has been blocked, nice try!";
        public string MESSAGE_DISCONNECT = "You have been disconnected from the server!";
        public string MESSAGE_BAN = "You have been banned from the server!";

        #endregion


        public bool Initialize() {
            bool flag = false;

            SettingsReader cfg = new SettingsReader(SETTINGS_PATH);

            if (cfg.Initialize()) {

                try {
                    cfg.GetSectionContent("general");

                    this.WindowName = cfg.Read<string>("general", "window_name");
                    this.BindPort = cfg.Read<int>("general", "bind_port");
                    this.DestIp = cfg.Read<string>("general", "dest_ip");
                    this.DestPort = cfg.Read<int>("general", "dest_port");
                    

                    this.ServerType = (ServerType)Enum.Parse(typeof(ServerType), cfg.Read<string>("general", "serv_type"));
                    

                    this.MaxConnServer = cfg.Read<int>("general", "max_conn_server");
                    this.MaxConnPerIp = cfg.Read<int>("general", "max_conn_ip");
                    this.ModuleConnTimeout = cfg.Read<int>("general", "module_conn_timeout");
                    this.SessActivityTimeout = cfg.Read<int>("general", "sess_activity_timeout");
                    this.SessProcThreads = cfg.Read<int>("general", "sess_proc_threads");
                    this.ListenerBacklog = cfg.Read<int>("general", "listener_backlog");
                    this.SessionBufferCount = cfg.Read<int>("general", "session_buffer_count");
                   

                    this.LogFolder = cfg.Read<string>("general", "log_folder");
                    this.LogTime = cfg.Read<int>("general", "log_time");
                   

                    this.LogNotifyColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), cfg.Read<string>("general", "log_notify_color"));
                    this.LogWarningColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), cfg.Read<string>("general", "log_warning_color"));
                    this.LogErrorColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), cfg.Read<string>("general", "log_error_color"));
                    this.ConsoleBackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), cfg.Read<string>("general", "con_bg_color"));

                    //Read redirection
                    int redirRuleCount = cfg.Read<int>("general", "redirect_count");

                    for (int i = 0; i < redirRuleCount; i++) {
                        string redirectSecitonName = string.Format("redirect_rule_{0}", i + 1);
                        //Will throw CoreException on failure
                        cfg.GetSectionContent(redirectSecitonName);

                        RedirectionRule rule = new RedirectionRule(
                            cfg.Read<string>(redirectSecitonName, "src_ip"),
                            cfg.Read<int>(redirectSecitonName, "src_port"),
                            cfg.Read<string>(redirectSecitonName, "dest_ip"),
                            cfg.Read<int>(redirectSecitonName, "dest_port"));

                        this.RedirectionRules.Add(rule);

                    }

                    //Ingame
                    this.DisableCaptcha = cfg.Read<bool>("game", "disable_captcha");
                    this.DisableCaptchaValue = cfg.Read<string>("game", "disable_captcha_value");
                    this.ExchangeDelay = cfg.Read<int>("game", "exchange_delay");
                    this.ExchangeDelayMsg = cfg.Read<string>("game", "exchange_delay_msg");
                    this.StallDelay = cfg.Read<int>("game", "stall_delay");
                    this.StallDelayMsg = cfg.Read<string>("game", "stall_delay_msg");
                    this.LogoutDelay = cfg.Read<int>("game", "logout_delay");
                    this.LogoutDelayMsg = cfg.Read<string>("game", "logout_delay_msg");
                    this.PlusLimit = cfg.Read<int>("game", "plus_limit");
                    this.PlusLimitMsg = cfg.Read<string>("game", "plus_limit_msg").Replace("%plus_limit%", this.PlusLimit.ToString());
                    this.GuildLimit = cfg.Read<int>("game", "guild_limit");
                    this.GuildLimitMsg = cfg.Read<string>("game", "guild_limit_msg").Replace("%guild_limit%", this.PlusLimit.ToString());
                    this.UnionLimit = cfg.Read<int>("game", "union_limit");
                    this.UnionLimitMsg = cfg.Read<string>("game", "union_limit_msg").Replace("%union_limit%", this.UnionLimit.ToString());
                    

                    this.CtfRegistrationMinLevel = cfg.Read<int>("game", "ctf_registration_min_lvl");
                    this.CtfRegistrationMinLvlMsg = cfg.Read<string>("game", "ctf_registration_min_lvl_msg");
                    this.CtfRegistrationMaxLevel = cfg.Read<int>("game", "ctf_registration_max_lvl");
                    this.CtfRegistrationMaxLevelMsg = cfg.Read<string>("game", "ctf_registration_max_lvl_msg");
                   

                    this.ArenaRegistrationMinLevel = cfg.Read<int>("game", "arena_registration_min_lvl");
                    this.ArenaRegistrationMinLevelMsg = cfg.Read<string>("game", "arena_registration_min_lvl_msg");
                    this.ArenaRegistrationMaxLevel = cfg.Read<int>("game", "arena_registration_max_lvl");
                    this.ArenaRegistrationMaxLevelMsg = cfg.Read<string>("game", "arena_registration_max_lvl_msg");
                   

                    this.DisableAcademyInvite = cfg.Read<bool>("game", "disable_academy_invite");
                    this.DisableAcademyInviteMsg = cfg.Read<string>("game", "disable_academy_invite_msg");

                    //SQL Connection
                    //Settings.sql = new SQL();
                    //bool sqlOk = true;//sql.Open("138.201.178.244,1433", "Arteus", "arteus@123", "SRO_VT_SHARD", true, 30);
                    //if (!sqlOk) {
                    //    Console.WriteLine("SQL Connection failed (before logger initialized)");
                    //    return loadAllSuccesss; //false
                    //} else {
                    //    Console.WriteLine("SQL Connection established (before logger initialized)");
                    //}

                    flag = true;
                } catch (Exception ex) {
                    Logger.IO.Print(LogLevel.Error, "[VSROSettings::Initialize()::0xVS1B100] -> There was an error while reading the settings file!");
                }
            }


            return flag;
        }

    }
}
