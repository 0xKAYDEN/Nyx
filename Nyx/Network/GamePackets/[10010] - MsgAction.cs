using System;

namespace Nyx.Server.Network.GamePackets
{
    public class Data : Writer, Interfaces.IPacket
    {
        public class CustomCommands
        {
            public const ushort
                ExitQuestion = 1,          
                Minimize = 2,
                ShowReviveButton = 1053,
                FlowerPointer = 1067,
                Enchant = 1091,
                LoginScreen = 1153,
                SelectRecipiet = 30,
                JoinGuild = 34,
                MakeFriend = 38,
                ChatWhisper = 40,
                CloseClient = 43,
                HotKey = 53,
                Furniture = 54,
                TQForum = 79,
                NewPokerGoldRoom = 167,
                PathFind = 97,
                LockItem = 102,
                ShowRevive = 1053,
                HideRevive = 1054,
                StatueMaker = 1066,
                GambleOpen = 1077,
                GambleClose = 1078,
                Compose = 1086,
                Craft1 = 1088,
                Craft2 = 1089,
                Warehouse = 1090,
                Inbox = 576,
                ShoppingMallShow = 1100,
                ShoppingMallHide = 1101,
                NoOfflineTraining = 1117,
                CenterClient = 1155,
                ClaimCP = 1197,
                ClaimAmount = 1198,
                MerchantApply = 1201,
                MerchantDone = 1202,
                RedeemEquipment = 1233,
                ClaimPrize = 1234,
                RepairAll = 1239,
                FlowerIcon = 1244,
                SendFlower = 1246,
                ReciveFlower = 1248,
                WarehouseVIP = 1272,
                UseExpBall = 1288,
                HackProtection = 1298,
                HideGUI = 1307,
                Inscribe = 3059,
                BuyPrayStone = 3069,
                HonorStore = 3104,
                Opponent = 3107,
                CountDownQualifier = 3109,
                QualifierStart = 3111,
                ItemsReturnedShow = 3117,
                ItemsReturnedWindow = 3118,
                ItemsReturnedHide = 3119,
                QuestFinished = 3147,
                QuestPoint = 3148,
                QuestPointSparkle = 3164,
                StudyPointsUp = 3192,
                PKTeamName = 3209,
                Updates = 3218,
                IncreaseLineage = 3227,
                HorseRacingStore = 3245,
                GuildPKTourny = 3249,
                QuitPK = 3251,
                Spectators = 3252,
                CardPlayOpen = 3270,
                CardPlayClost = 3271,
                ArtifactPurification = 3344,
                SafeguardConvoyShow = 3389,
                SafeguardConvoyHide = 3390,
                RefineryStabilization = 3392,
                ArtifactStabilization = 3398,
                SmallChat = 3406,
                NormalChat = 3407,
                Reincarnation = 3439,
                CTFFlag = 3211,
                CTFEnd = 3549,
                CTFScores = 3359;

        }
        public uint Data28_Uint//For Poker  
        {
            get { return BitConverter.ToUInt32(Buffer, 28); }
            set { Write(value, 28, Buffer); }
        }
        public byte DailyQuestWordLenght//For Poker  
        {
            get { return Buffer[42]; }
            set { Buffer[42] = value; }
        }
        public string DailyQuestWord//For Poker  
        {
            get { return System.Text.UnicodeEncoding.UTF8.GetString(Buffer, 43, DailyQuestWordLenght); }
            set { Writer.Write(value, 43, Buffer); }
        } 
        public uint wParam5
        {
            get
            {
                return BitConverter.ToUInt32(this.Buffer, 32 + 4);
            }
            set
            {
                Writer.Write(value, 32 + 4, this.Buffer);
            }
        }
        public class WindowCommands
        {
            public const ushort
                Compose = 1,
                Craft = 2,
                Warehouse = 4,
                DetainRedeem = 336,
                DetainClaim = 337,
                VIPWarehouse = 341,
                Breeding = 368,
                PurificationWindow = 455,
                StabilizationWindow = 459,
                JiangHuSetName = 0x269,
                TalismanUpgrade = 347,
                GemComposing = 422,
                OpenSockets = 425,
                Blessing = 426,
                TortoiseGemComposing = 438,
                RefineryStabilization = 448,
                HorseRacingStore = 464,
                Reincarnation = 485,
                SecondaryPasswordVerification = 568;

        }
        public const ushort
                ObserveKnownPerson = 54,
                SetLocation = 74,
                Hotkeys = 75,
                ConfirmFriends = 76,
                ConfirmProficiencies = 77,
                ConfirmSpells = 78,
                ChangeDirection = 79,
                ChangeAction = 81,
                UsePortal = 85,
                Teleport = 86,
                Leveled = 92,
                XPListEnd = 93,
                Revive = 94,
                DeleteCharacter = 95,
                ChangePKMode = 96,
                ConfirmGuild = 97,
                SwingPickaxe = 99,
                UnknownEntity = 102,
                TeamSearchForMember = 106,
                NewCoordonates = 108,
                OwnBooth = 111,
                GetSurroundings = 114,
                OpenCustom = 116,
                PokerTournament = 167,
                ObserveEquipment = 117,
                PathFinding = 162,
                EndTransformation = 118,
                EndFly = 120,
                ViewEnemyInfo = 123,
                OpenWindow = 126,
                CompleteLogin = 251,
                RemoveEntity = 135,
                AddEntity = 134,
                Jump = 137,
                Die = 145,
                EndTeleport = 146,
                ViewFriendInfo = 148,
                ChangeFace = 151,
                ViewPartnerInfo = 152,
                Confiscator = 153,
                FlashStep = 156,
                OpenShop = 160,
                CountDown = 159,
                Away = 161,
                AppearanceType = 178,
                LevelUpSpell = 252,
                LevelUpProficiency = 253,
                ObserveEquipment2 = 310,
                BeginSteedRace = 401,
                FirstCredit = 255,
                DailyQuestSure = 220,
                DailyQuestSure2 = 164,
                FinishSteedRace = 402,
                DetainWindowRequest = 153;
                //JiangHu = 0x7e;

        public static Data Custom(uint customType, uint uid, ushort wParam1 = 0, ushort wParam2 = 0)
        {
            Data data = new Data(true);
            data.ID = Data.OpenCustom;
            data.UID = uid;
            data.TimeStamp = Time32.Now;
            data.dwParam = customType;
            data.wParam1 = wParam1;
            data.wParam2 = wParam2;
            return data;

        }
        public Data(bool Create)
        {
            if (Create)
            {
                Buffer = new byte[41 + 8];
                Writer.Write(41, 0, Buffer);
                Writer.Write(10010, 2, Buffer);
                Write((uint)Time32.timeGetTime().GetHashCode(), 4, Buffer);
            }
        }
        public Data(uint Identifier, uint Value1, ushort Value2, ushort Value3, ushort Type)
        {
            byte[] Buffer = new byte[8 + 32];
            Writer.Write((ushort)(Buffer.Length - 8), 0, Buffer);
            Writer.Write((ushort)10010, 2, Buffer);
            Write((uint)Time32.timeGetTime().GetHashCode(), 4, Buffer);
           Writer.Write((int)Identifier,8,Buffer);
           Writer.Write((int)Value1,12,Buffer);
           Writer.Write(Type, 14, Buffer);
            Writer.Write(Value2, 16, Buffer);
            Writer.Write(Value3, 18, Buffer);
  
        }
        byte[] Buffer;
        public uint UID
        {
            get { return BitConverter.ToUInt32(Buffer, 8); }
            set { Writer.Write(value, 8, Buffer); }
        }
        public uint ForShopId
        {
            get
            {
                return BitConverter.ToUInt16(Buffer, 20);
            }
            set
            {
                Write(value, 20, Buffer);
            }
        }
        #region dwParam
        /// <summary>
        /// Offset [8/0x08]
        /// </summary>
        public ushort X
        {
            get { return (ushort)dwParam; }
            set { dwParam = (uint)((Y   << 16) | value); }
        }

        /// <summary>
        /// Offset [10/0x0a]
        /// </summary>
        public ushort Y
        {
            get { return (ushort)(dwParam >> 16); }
            set { dwParam = (uint)((value << 16) | X); }
        }

        #endregion
        public uint dwParam
        {
            get
            {
                return BitConverter.ToUInt32(Buffer, 12);
            }
            set
            {
                Write(value, 12, Buffer);
            }
        }
        public uint Data24_Uint
        {
            get
            {
                return BitConverter.ToUInt32(Buffer, 28);
            }
            set
            {
                Write(value, 28, Buffer);
            }
        }
        public ushort wParam7
        {
            get
            {
                return BitConverter.ToUInt16(Buffer, 32);
            }
            set
            {
                Writer.Write(value, 32, Buffer);
            }
        }
        public uint TimeStamp2
        {
            get
            {
                return BitConverter.ToUInt32(Buffer, 20);
            }
            set
            {
                Write(value, 20, Buffer);
            }
        }   
        public ushort wParam6
        {
            get
            {
                return BitConverter.ToUInt16(Buffer, 14);
            }
            set
            {
                Writer.Write(value, 14, Buffer);
            }
        }
        public Time32 TimeStamp
        {
            get
            {
                return new Time32(BitConverter.ToUInt32(Buffer, 20));
            }
            set
            {
                Write((uint)value.GetHashCode(), 20, Buffer);
            }
        }
        public Time32 TimeStamp22
        {
            get
            {
                return new Time32(BitConverter.ToUInt32(Buffer, 20));
            }
            set
            {
                Write((uint)value.GetHashCode(), 20, Buffer);
            }
        }
        public ushort X2
        {
            get
            {
                return BitConverter.ToUInt16(Buffer, 28);
            }
            set
            {
                Writer.Write(value, 28, Buffer);
            }
        }
        public ushort Y2
        {
            get
            {
                return BitConverter.ToUInt16(Buffer, 30);
            }
            set
            {
                Writer.Write(value, 30, Buffer);
            }
        }
        public uint ID
        {
            get
            {
                return BitConverter.ToUInt16(Buffer, 24);
            }
            set
            {
                Write(value, 24, Buffer);
            }
        }

        public Game.Enums.ConquerAngle Facing
        {
            get
            {
                return (Game.Enums.ConquerAngle)Buffer[26];
            }
            set
            {
                Buffer[26] = (byte)(Game.Enums.ConquerAngle)value;
            }
        }
        public Game.Enums.ConquerAngle Facing2
        {
            get
            {
                return (Game.Enums.ConquerAngle)Buffer[26];
            }
            set
            {
                Buffer[26] = 2;
            }
        }
        public ushort wParam1
        {
            get
            {
                return BitConverter.ToUInt16(Buffer, 28);
            }
            set
            {
                Writer.Write(value, 28, Buffer);
            }
        }
        public byte Unknown
        {
            get
            {
                return Buffer[36];
            }
            set
            {
                Buffer[36] = value;
                Buffer[41] = 1;
            }
        }
        public byte Unknown2
        {
           get
            {
                return Buffer[41];
            }
            set
            {
                Buffer[41] = value;
            }
        }
        public ushort wParam2
        {
            get
            {
                return BitConverter.ToUInt16(Buffer, 30);
            }
            set
            {
                Writer.Write(value, 30, Buffer);
            }
        }

        public uint wParam3
        {
            get
            {
                return BitConverter.ToUInt32(Buffer, 28);
            }
            set
            {
                Write(value, 28, Buffer);
            }
        }

        public uint wParam4
        {
            get
            {
                return BitConverter.ToUInt32(Buffer, 30);
            }
            set
            {
                Write(value, 30, Buffer);
            }
        }

        public void Deserialize(byte[] buffer)
        {
            this.Buffer = buffer;
        }
        public byte[] Encode()
        {
            return Buffer;
        }
        public void Send(Client.GameClient client)
        {
            client.Send(Buffer);
        }
    }
}
