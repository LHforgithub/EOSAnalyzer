using Microsoft.CodeAnalysis;

namespace EOSAnalyzer
{
    public static class EOSAnalyzerDataBase
    {
        public const string EOSEventCodeAttribute = "EventCode";
        public const string EOSEventCodeMethodAttribute = "EventCodeMethod";
        public const string EOSIEvntCode = "IEventCode";
        public const string EOSNoEventCodeClassAttribute = "NoEventCodeClass";
        public const string EOSRuleTitle = "Rule_EOS";

        public const int Severity_Error = (int)DiagnosticSeverity.Error;
        public const int Severity_Hidden = (int)DiagnosticSeverity.Hidden;
        public const int Severity_Info = (int)DiagnosticSeverity.Info;
        public const int Severity_Warning = (int)DiagnosticSeverity.Warning;

        #region EOS Code Diagnostic

        /// <summary>
        /// 声明了事件码的类型中缺少对事件方法的定义的声明
        /// </summary>
        public const string DiagnosticId_EOS001C = "EOS001C";

        /// <summary>
        /// 不能将属性方法作为事件方法定义的声明
        /// </summary>
        public const string DiagnosticId_EOS002C = "EOS002C";

        /// <summary>
        /// 过多的事件方法定义声明
        /// </summary>
        public const string DiagnosticId_EOS003C = "EOS003C";

        /// <summary>
        /// 声明了事件方法的定义但所在类型未声明为事件码
        /// </summary>
        public const string DiagnosticId_EOS004C = "EOS004C";

        /// <summary>
        /// 不能将泛型方法作为事件方法定义的声明
        /// </summary>
        public const string DiagnosticId_EOS005C = "EOS005C";

        /// <summary>
        /// 不能将泛型类型作为事件码的声明
        /// </summary>
        public const string DiagnosticId_EOS006C = "EOS006C";

        public static readonly LocalizableString Description_001C = new LocalizableResourceString(nameof(Resources.EOS001C_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_002C = new LocalizableResourceString(nameof(Resources.EOS002C_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_003C = new LocalizableResourceString(nameof(Resources.EOS003C_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_004C = new LocalizableResourceString(nameof(Resources.EOS004C_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_005C = new LocalizableResourceString(nameof(Resources.EOS005C_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_006C = new LocalizableResourceString(nameof(Resources.EOS006C_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_001C = new LocalizableResourceString(nameof(Resources.EOS001C_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_002C = new LocalizableResourceString(nameof(Resources.EOS002C_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_003C = new LocalizableResourceString(nameof(Resources.EOS003C_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_004C = new LocalizableResourceString(nameof(Resources.EOS004C_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_005C = new LocalizableResourceString(nameof(Resources.EOS005C_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_006C = new LocalizableResourceString(nameof(Resources.EOS006C_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_001C = new LocalizableResourceString(nameof(Resources.EOS001C_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_002C = new LocalizableResourceString(nameof(Resources.EOS002C_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_003C = new LocalizableResourceString(nameof(Resources.EOS003C_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_004C = new LocalizableResourceString(nameof(Resources.EOS004C_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_005C = new LocalizableResourceString(nameof(Resources.EOS005C_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_006C = new LocalizableResourceString(nameof(Resources.EOS006C_Title), Resources.ResourceManager, typeof(Resources));
        public static int DiagnosticSeverity_EOS001C = Severity_Error;
        public static int DiagnosticSeverity_EOS002C = Severity_Error;

        public static int DiagnosticSeverity_EOS003C = Severity_Warning;

        public static int DiagnosticSeverity_EOS004C = Severity_Warning;

        public static int DiagnosticSeverity_EOS005C = Severity_Error;

        public static int DiagnosticSeverity_EOS006C = Severity_Error;

        /// <summary>
        /// 声明了事件码的类型中缺少对事件方法的定义的声明
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS001C =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS001C,
                Title_001C,
                MessageFormat_001C,
                 "Attribute",
                (DiagnosticSeverity)DiagnosticSeverity_EOS001C,
                isEnabledByDefault: true,
                description: Description_001C
                );

        /// <summary>
        /// 不能将属性方法作为事件方法定义的声明
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS002C =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS002C,
                Title_002C,
                MessageFormat_002C,
                 "Attribute",
                (DiagnosticSeverity)DiagnosticSeverity_EOS002C,
                isEnabledByDefault: true,
                description: Description_002C
                );

        /// <summary>
        /// 过多的事件方法定义声明
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS003C =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS003C,
                Title_003C,
                MessageFormat_003C,
                 "Attribute",
                (DiagnosticSeverity)DiagnosticSeverity_EOS003C,
                isEnabledByDefault: true,
                description: Description_003C
                );

        /// <summary>
        /// 声明了事件方法的定义但所在类型未声明为事件码
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS004C =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS004C,
                Title_004C,
                MessageFormat_004C,
                 "Attribute",
                (DiagnosticSeverity)DiagnosticSeverity_EOS004C,
                isEnabledByDefault: true,
                description: Description_004C
                );

        /// <summary>
        /// 不能将泛型方法作为事件方法定义的声明
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS005C =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS005C,
                Title_005C,
                MessageFormat_005C,
                 "Attribute",
                (DiagnosticSeverity)DiagnosticSeverity_EOS005C,
                isEnabledByDefault: true,
                description: Description_005C
                );

        /// <summary>
        /// 不能将泛型类型作为事件码的声明
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS006C =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS006C,
                Title_006C,
                MessageFormat_006C,
                 "Attribute",
                (DiagnosticSeverity)DiagnosticSeverity_EOS006C,
                isEnabledByDefault: true,
                description: Description_006C
                );

        #endregion EOS Code Diagnostic

        #region EOS Listener Diagnostic

        /// <summary>
        /// 声明的事件方法接收者缺少指向的事件码
        /// </summary>
        public const string DiagnosticId_EOS001L = "EOS001L";

        /// <summary>
        /// 事件方法指向的事件码类型参数未声明
        /// </summary>
        public const string DiagnosticId_EOS002L = "EOS002L";

        /// <summary>
        /// 指向的事件码类型未声明事件方法定义
        /// </summary>
        public const string DiagnosticId_EOS003L = "EOS003L";

        /// <summary>
        /// 事件方法的参数数量与指向的事件方法定义的参数数量不符
        /// </summary>
        public const string DiagnosticId_EOS004L = "EOS004L";

        /// <summary>
        /// 事件方法的参数类型与指向的事件方法定义的参数类型不符
        /// </summary>
        public const string DiagnosticId_EOS005L = "EOS005L";

        /// <summary>
        /// 声明了事件接收者的类型内没有任何事件方法
        /// </summary>
        public const string DiagnosticId_EOS006L = "EOS006L";

        /// <summary>
        /// 声明了事件方法但所在类型未声明为事件接收者
        /// </summary>
        public const string DiagnosticId_EOS007L = "EOS007L";

        /// <summary>
        /// 事件方法的参数类型必须为事件方法定义的参数类型的基类或接口，而非相反
        /// </summary>
        public const string DiagnosticId_EOS008L = "EOS008L";

        /// <summary>
        /// 泛型类型不能作为事件接收者使用
        /// </summary>
        public const string DiagnosticId_EOS009L = "EOS009L";

        /// <summary>
        /// 类型中已有指向了相同事件码的事件方法
        /// </summary>
        public const string DiagnosticId_EOS010L = "EOS010L";

        public static readonly LocalizableString Description_001L = new LocalizableResourceString(nameof(Resources.EOS001L_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_002L = new LocalizableResourceString(nameof(Resources.EOS002L_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_003L = new LocalizableResourceString(nameof(Resources.EOS003L_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_004L = new LocalizableResourceString(nameof(Resources.EOS004L_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_005L = new LocalizableResourceString(nameof(Resources.EOS005L_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_006L = new LocalizableResourceString(nameof(Resources.EOS006L_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_007L = new LocalizableResourceString(nameof(Resources.EOS007L_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_008L = new LocalizableResourceString(nameof(Resources.EOS008L_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_009L = new LocalizableResourceString(nameof(Resources.EOS009L_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_010L = new LocalizableResourceString(nameof(Resources.EOS010L_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_001L = new LocalizableResourceString(nameof(Resources.EOS001L_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_002L = new LocalizableResourceString(nameof(Resources.EOS002L_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_003L = new LocalizableResourceString(nameof(Resources.EOS003L_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_004L = new LocalizableResourceString(nameof(Resources.EOS004L_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_005L = new LocalizableResourceString(nameof(Resources.EOS005L_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_006L = new LocalizableResourceString(nameof(Resources.EOS006L_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_007L = new LocalizableResourceString(nameof(Resources.EOS007L_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_008L = new LocalizableResourceString(nameof(Resources.EOS008L_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_009L = new LocalizableResourceString(nameof(Resources.EOS009L_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_010L = new LocalizableResourceString(nameof(Resources.EOS010L_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_001L = new LocalizableResourceString(nameof(Resources.EOS001L_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_002L = new LocalizableResourceString(nameof(Resources.EOS002L_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_003L = new LocalizableResourceString(nameof(Resources.EOS003L_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_004L = new LocalizableResourceString(nameof(Resources.EOS004L_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_005L = new LocalizableResourceString(nameof(Resources.EOS005L_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_006L = new LocalizableResourceString(nameof(Resources.EOS006L_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_007L = new LocalizableResourceString(nameof(Resources.EOS007L_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_008L = new LocalizableResourceString(nameof(Resources.EOS008L_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_009L = new LocalizableResourceString(nameof(Resources.EOS009L_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_010L = new LocalizableResourceString(nameof(Resources.EOS010L_Title), Resources.ResourceManager, typeof(Resources));
        public static int DiagnosticSeverity_EOS001L = Severity_Error;
        public static int DiagnosticSeverity_EOS002L = Severity_Warning;

        public static int DiagnosticSeverity_EOS003L = Severity_Warning;

        public static int DiagnosticSeverity_EOS004L = Severity_Error;

        public static int DiagnosticSeverity_EOS005L = Severity_Error;

        public static int DiagnosticSeverity_EOS006L = Severity_Info;

        public static int DiagnosticSeverity_EOS007L = Severity_Warning;

        public static int DiagnosticSeverity_EOS008L = Severity_Error;

        public static int DiagnosticSeverity_EOS009L = Severity_Error;

        public static int DiagnosticSeverity_EOS010L = Severity_Error;

        /// <summary>
        /// 声明的事件方法接收者缺少指向的事件码
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS001L =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS001L,
                Title_001L,
                MessageFormat_001L,
                 "Attribute",
                (DiagnosticSeverity)DiagnosticSeverity_EOS001L,
                isEnabledByDefault: true,
                description: Description_001L
                );

        /// <summary>
        /// 事件方法指向的事件码类型参数未声明
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS002L =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS002L,
                Title_002L,
                MessageFormat_002L,
                 "Attribute",
                (DiagnosticSeverity)DiagnosticSeverity_EOS002L,
                isEnabledByDefault: true,
                description: Description_002L
                );

        /// <summary>
        /// 指向的事件码类型未声明事件方法定义
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS003L =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS003L,
                Title_003L,
                MessageFormat_003L,
                 "Attribute",
                (DiagnosticSeverity)DiagnosticSeverity_EOS003L,
                isEnabledByDefault: true,
                description: Description_003L
                );

        /// <summary>
        /// 事件方法的参数数量与指向的事件方法定义的参数数量不符
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS004L =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS004L,
                Title_004L,
                MessageFormat_004L,
                 "Attribute",
                (DiagnosticSeverity)DiagnosticSeverity_EOS004L,
                isEnabledByDefault: true,
                description: Description_004L
                );

        /// <summary>
        /// 事件方法的参数类型与指向的事件方法定义的参数类型不符
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS005L =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS005L,
                Title_005L,
                MessageFormat_005L,
                 "Attribute",
                (DiagnosticSeverity)DiagnosticSeverity_EOS005L,
                isEnabledByDefault: true,
                description: Description_005L
                );

        /// <summary>
        /// 声明了事件接收者的类型内没有任何事件方法
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS006L =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS006L,
                Title_006L,
                MessageFormat_006L,
                 "Attribute",
                (DiagnosticSeverity)DiagnosticSeverity_EOS006L,
                isEnabledByDefault: true,
                description: Description_006L
                );

        /// <summary>
        /// 声明了事件方法但所在类型未声明为事件接收者
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS007L =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS007L,
                Title_007L,
                MessageFormat_007L,
                 "Attribute",
                (DiagnosticSeverity)DiagnosticSeverity_EOS007L,
                isEnabledByDefault: true,
                description: Description_007L
                );

        /// <summary>
        /// 事件方法的参数类型必须为事件方法定义的参数类型的基类或接口，而非相反
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS008L =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS008L,
                Title_008L,
                MessageFormat_008L,
                 "Attribute",
                (DiagnosticSeverity)DiagnosticSeverity_EOS008L,
                isEnabledByDefault: true,
                description: Description_008L
                );

        /// <summary>
        /// 泛型类型不能作为事件接收者使用
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS009L =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS009L,
                Title_009L,
                MessageFormat_009L,
                 "Attribute",
                (DiagnosticSeverity)DiagnosticSeverity_EOS009L,
                isEnabledByDefault: true,
                description: Description_009L
                );

        /// <summary>
        /// 类型中已有指向了相同事件码的事件方法
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS010L =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS010L,
                Title_010L,
                MessageFormat_010L,
                 "Attribute",
                (DiagnosticSeverity)DiagnosticSeverity_EOS010L,
                isEnabledByDefault: true,
                description: Description_010L
                );

        #endregion EOS Listener Diagnostic

        #region EOS BroadCast Diagnostic

        /// <summary>
        /// 尝试广播的事件码未定义
        /// </summary>
        public const string DiagnosticId_EOS001B = "EOS001B";

        /// <summary>
        /// 未输入应当广播的事件的事件码
        /// </summary>
        public const string DiagnosticId_EOS002B = "EOS002B";

        /// <summary>
        /// 不推荐直接使用字符串作为事件码
        /// </summary>
        public const string DiagnosticId_EOS003B = "EOS003B";

        /// <summary>
        /// 建议使用常量值类型作为事件码
        /// </summary>
        public const string DiagnosticId_EOS004B = "EOS004B";

        /// <summary>
        /// 尝试广播的事件填入的参数过多
        /// </summary>
        public const string DiagnosticId_EOS005B = "EOS005B";

        /// <summary>
        /// 事件广播时填入的参数类型与定义的类型不符
        /// </summary>
        public const string DiagnosticId_EOS006B = "EOS006B";

        /// <summary>
        /// 尝试广播的事件填入的参数不足
        /// </summary>
        public const string DiagnosticId_EOS007B = "EOS007B";

        /// <summary>
        /// 事件广播中有可选填入的参数存在
        /// </summary>
        public const string DiagnosticId_EOS008B = "EOS008B";

        /// <summary>
        /// 事件广播参数输入参数为数组
        /// </summary>
        public const string DiagnosticId_EOS009B = "EOS009B";

        /// <summary>
        /// 事件广播中可能的null引用参数
        /// </summary>
        public const string DiagnosticId_EOS010B = "EOS010B";

        /// <summary>
        /// 事件广播中填入的参数必须为事件方法定义中的参数类型的子类或继承，而非相反
        /// </summary>
        public const string DiagnosticId_EOS011B = "EOS011B";

        public static readonly LocalizableString Description_001B = new LocalizableResourceString(nameof(Resources.EOS001B_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_002B = new LocalizableResourceString(nameof(Resources.EOS002B_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_003B = new LocalizableResourceString(nameof(Resources.EOS003B_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_004B = new LocalizableResourceString(nameof(Resources.EOS004B_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_005B = new LocalizableResourceString(nameof(Resources.EOS005B_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_006B = new LocalizableResourceString(nameof(Resources.EOS006B_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_007B = new LocalizableResourceString(nameof(Resources.EOS007B_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_008B = new LocalizableResourceString(nameof(Resources.EOS008B_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_009B = new LocalizableResourceString(nameof(Resources.EOS009B_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_010B = new LocalizableResourceString(nameof(Resources.EOS010B_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_011B = new LocalizableResourceString(nameof(Resources.EOS011B_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_001B = new LocalizableResourceString(nameof(Resources.EOS001B_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_002B = new LocalizableResourceString(nameof(Resources.EOS002B_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_003B = new LocalizableResourceString(nameof(Resources.EOS003B_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_004B = new LocalizableResourceString(nameof(Resources.EOS004B_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_005B = new LocalizableResourceString(nameof(Resources.EOS005B_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_006B = new LocalizableResourceString(nameof(Resources.EOS006B_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_007B = new LocalizableResourceString(nameof(Resources.EOS007B_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_008B = new LocalizableResourceString(nameof(Resources.EOS008B_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_009B = new LocalizableResourceString(nameof(Resources.EOS009B_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_010B = new LocalizableResourceString(nameof(Resources.EOS010B_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_011B = new LocalizableResourceString(nameof(Resources.EOS011B_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_001B = new LocalizableResourceString(nameof(Resources.EOS001B_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_002B = new LocalizableResourceString(nameof(Resources.EOS002B_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_003B = new LocalizableResourceString(nameof(Resources.EOS003B_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_004B = new LocalizableResourceString(nameof(Resources.EOS004B_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_005B = new LocalizableResourceString(nameof(Resources.EOS005B_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_006B = new LocalizableResourceString(nameof(Resources.EOS006B_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_007B = new LocalizableResourceString(nameof(Resources.EOS007B_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_008B = new LocalizableResourceString(nameof(Resources.EOS008B_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_009B = new LocalizableResourceString(nameof(Resources.EOS009B_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_010B = new LocalizableResourceString(nameof(Resources.EOS010B_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_011B = new LocalizableResourceString(nameof(Resources.EOS011B_Title), Resources.ResourceManager, typeof(Resources));
        public static int DiagnosticSeverity_EOS001B = Severity_Warning;
        public static int DiagnosticSeverity_EOS002B = Severity_Error;

        public static int DiagnosticSeverity_EOS003B = Severity_Warning;

        public static int DiagnosticSeverity_EOS004B = Severity_Warning;

        public static int DiagnosticSeverity_EOS005B = Severity_Info;

        public static int DiagnosticSeverity_EOS006B = Severity_Error;

        public static int DiagnosticSeverity_EOS007B = Severity_Error;

        public static int DiagnosticSeverity_EOS008B = Severity_Info;

        public static int DiagnosticSeverity_EOS009B = Severity_Info;

        public static int DiagnosticSeverity_EOS010B = Severity_Warning;

        public static int DiagnosticSeverity_EOS011B = Severity_Error;

        /// <summary>
        /// 尝试广播的事件码未定义
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS001B =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS001B,
                Title_001B,
                MessageFormat_001B,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS001B,
                isEnabledByDefault: true,
                description: Description_001B
                );

        /// <summary>
        /// 未输入应当广播的事件的事件码
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS002B =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS002B,
                Title_002B,
                MessageFormat_002B,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS002B,
                isEnabledByDefault: true,
                description: Description_002B
                );

        /// <summary>
        /// 不推荐直接使用字符串作为事件码
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS003B =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS003B,
                Title_003B,
                MessageFormat_003B,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS003B,
                isEnabledByDefault: true,
                description: Description_003B
                );

        /// <summary>
        /// 建议使用常量值类型作为事件码
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS004B =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS004B,
                Title_004B,
                MessageFormat_004B,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS004B,
                isEnabledByDefault: true,
                description: Description_004B
                );

        /// <summary>
        /// 尝试广播的事件填入的参数过多
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS005B =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS005B,
                Title_005B,
                MessageFormat_005B,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS005B,
                isEnabledByDefault: true,
                description: Description_005B
                );

        /// <summary>
        /// 事件广播时填入的参数类型与定义的类型不符
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS006B =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS006B,
                Title_006B,
                MessageFormat_006B,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS006B,
                isEnabledByDefault: true,
                description: Description_006B
                );

        /// <summary>
        /// 尝试广播的事件填入的参数不足
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS007B =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS007B,
                Title_007B,
                MessageFormat_007B,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS007B,
                isEnabledByDefault: true,
                description: Description_007B
                );

        /// <summary>
        /// 事件广播中有可选填入的参数存在
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS008B =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS008B,
                Title_008B,
                MessageFormat_008B,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS008B,
                isEnabledByDefault: true,
                description: Description_008B
                );

        /// <summary>
        /// 事件广播参数输入参数为数组
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS009B =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS009B,
                Title_009B,
                MessageFormat_009B,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS009B,
                isEnabledByDefault: true,
                description: Description_009B
                );

        /// <summary>
        /// 事件广播中可能的null引用参数
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS010B =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS010B,
                Title_010B,
                MessageFormat_010B,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS010B,
                isEnabledByDefault: true,
                description: Description_010B
                );

        /// <summary>
        /// 事件广播中填入的参数必须为事件方法定义中的参数类型的子类或继承，而非相反
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS011B =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS011B,
                Title_011B,
                MessageFormat_011B,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS011B,
                isEnabledByDefault: true,
                description: Description_011B
                );

        #endregion EOS BroadCast Diagnostic

        #region EOS Add Or Remove Listener Diagnostic

        /// <summary>
        /// 尝试添加或移除事件接收者的事件码未定义
        /// </summary>
        public const string DiagnosticId_EOS001AOR = "EOS001AOR";

        /// <summary>
        /// 确保添加或移除事件时输入的方法名称和参数定义可以被反射查找
        /// </summary>
        public const string DiagnosticId_EOS002AOR = "EOS002AOR";

        /// <summary>
        /// 添加或移除事件接收者时，试图添加或移除项目上下文不支持的类型或null引用
        /// </summary>
        public const string DiagnosticId_EOS003AOR = "EOS003AOR";

        /// <summary>
        /// 添加或移除事件接收者时，输入参数对应的类型不是已定义的事件接收者类型
        /// </summary>
        public const string DiagnosticId_EOS004AOR = "EOS004AOR";

        /// <summary>
        /// 添加或移除事件接收者时，输入参数对应的类型中，定义的对应于此事件码的事件方法数量错误
        /// </summary>
        public const string DiagnosticId_EOS005AOR = "EOS005AOR";

        /// <summary>
        /// 缺少事件接收者对象
        /// </summary>
        public const string DiagnosticId_EOS006AOR = "EOS006AOR";

        /// <summary>
        /// 建议使用常量值类型作为输入的事件接收者类型
        /// </summary>
        public const string DiagnosticId_EOS007AOR = "EOS007AOR";

        /// <summary>
        /// 非静态类型事件接收者实例参数不能为null
        /// </summary>
        public const string DiagnosticId_EOS008AOR = "EOS008AOR";

        /// <summary>
        /// 输入的事件接收者类型和输入的事件接收者实例类型不一致
        /// </summary>
        public const string DiagnosticId_EOS009AOR = "EOS009AOR";

        public static readonly LocalizableString Description_001AOR = new LocalizableResourceString(nameof(Resources.EOS001AOR_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_002AOR = new LocalizableResourceString(nameof(Resources.EOS002AOR_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_003AOR = new LocalizableResourceString(nameof(Resources.EOS003AOR_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_004AOR = new LocalizableResourceString(nameof(Resources.EOS004AOR_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_005AOR = new LocalizableResourceString(nameof(Resources.EOS005AOR_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_006AOR = new LocalizableResourceString(nameof(Resources.EOS006AOR_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_007AOR = new LocalizableResourceString(nameof(Resources.EOS007AOR_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_008AOR = new LocalizableResourceString(nameof(Resources.EOS008AOR_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Description_009AOR = new LocalizableResourceString(nameof(Resources.EOS009AOR_Description), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_001AOR = new LocalizableResourceString(nameof(Resources.EOS001AOR_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_002AOR = new LocalizableResourceString(nameof(Resources.EOS002AOR_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_003AOR = new LocalizableResourceString(nameof(Resources.EOS003AOR_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_004AOR = new LocalizableResourceString(nameof(Resources.EOS004AOR_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_005AOR = new LocalizableResourceString(nameof(Resources.EOS005AOR_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_006AOR = new LocalizableResourceString(nameof(Resources.EOS006AOR_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_007AOR = new LocalizableResourceString(nameof(Resources.EOS007AOR_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_008AOR = new LocalizableResourceString(nameof(Resources.EOS008AOR_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString MessageFormat_009AOR = new LocalizableResourceString(nameof(Resources.EOS009AOR_MessageFormat), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_001AOR = new LocalizableResourceString(nameof(Resources.EOS001AOR_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_002AOR = new LocalizableResourceString(nameof(Resources.EOS002AOR_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_003AOR = new LocalizableResourceString(nameof(Resources.EOS003AOR_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_004AOR = new LocalizableResourceString(nameof(Resources.EOS004AOR_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_005AOR = new LocalizableResourceString(nameof(Resources.EOS005AOR_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_006AOR = new LocalizableResourceString(nameof(Resources.EOS006AOR_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_007AOR = new LocalizableResourceString(nameof(Resources.EOS007AOR_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_008AOR = new LocalizableResourceString(nameof(Resources.EOS008AOR_Title), Resources.ResourceManager, typeof(Resources));
        public static readonly LocalizableString Title_009AOR = new LocalizableResourceString(nameof(Resources.EOS009AOR_Title), Resources.ResourceManager, typeof(Resources));
        public static int DiagnosticSeverity_EOS001AOR = Severity_Warning;
        public static int DiagnosticSeverity_EOS002AOR = Severity_Info;

        public static int DiagnosticSeverity_EOS003AOR = Severity_Error;

        public static int DiagnosticSeverity_EOS004AOR = Severity_Error;

        public static int DiagnosticSeverity_EOS005AOR = Severity_Warning;

        public static int DiagnosticSeverity_EOS006AOR = Severity_Error;

        public static int DiagnosticSeverity_EOS007AOR = Severity_Warning;

        public static int DiagnosticSeverity_EOS008AOR = Severity_Error;

        public static int DiagnosticSeverity_EOS009AOR = Severity_Error;

        /// <summary>
        /// 尝试添加或移除事件接收者的事件码未定义
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS001AOR =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS001AOR,
                Title_001AOR,
                MessageFormat_001AOR,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS001AOR,
                isEnabledByDefault: true,
                description: Description_001AOR
                );

        /// <summary>
        /// 确保添加或移除事件时输入的方法名称和参数定义可以被反射查找
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS002AOR =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS002AOR,
                Title_002AOR,
                MessageFormat_002AOR,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS002AOR,
                isEnabledByDefault: true,
                description: Description_002AOR
                );

        /// <summary>
        /// 添加或移除事件接收者时，试图添加或移除项目上下文不支持的类型或null引用
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS003AOR =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS003AOR,
                Title_003AOR,
                MessageFormat_003AOR,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS003AOR,
                isEnabledByDefault: true,
                description: Description_003AOR
                );

        /// <summary>
        /// 添加或移除事件接收者时，输入参数对应的类型不是已定义的事件接收者类型
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS004AOR =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS004AOR,
                Title_004AOR,
                MessageFormat_004AOR,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS004AOR,
                isEnabledByDefault: true,
                description: Description_004AOR
                );

        /// <summary>
        /// 添加或移除事件接收者时，输入参数对应的类型中，定义的对应于此事件码的事件方法数量错误
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS005AOR =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS005AOR,
                Title_005AOR,
                MessageFormat_005AOR,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS005AOR,
                isEnabledByDefault: true,
                description: Description_005AOR
                );

        /// <summary>
        /// 缺少事件接收者对象
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS006AOR =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS006AOR,
                Title_006AOR,
                MessageFormat_006AOR,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS006AOR,
                isEnabledByDefault: true,
                description: Description_006AOR
                );

        /// <summary>
        /// 建议使用常量值类型作为输入的事件接收者类型
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS007AOR =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS007AOR,
                Title_007AOR,
                MessageFormat_007AOR,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS007AOR,
                isEnabledByDefault: true,
                description: Description_007AOR
                );

        /// <summary>
        /// 非静态类型事件接收者实例参数不能为null
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS008AOR =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS008AOR,
                Title_008AOR,
                MessageFormat_008AOR,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS008AOR,
                isEnabledByDefault: true,
                description: Description_008AOR
                );

        /// <summary>
        /// 输入的事件接收者类型和输入的事件接收者实例类型不一致
        /// </summary>
        public static DiagnosticDescriptor Rule_EOS009AOR =>
            new DiagnosticDescriptor(
                DiagnosticId_EOS009AOR,
                Title_009AOR,
                MessageFormat_009AOR,
                 "Method",
                (DiagnosticSeverity)DiagnosticSeverity_EOS009AOR,
                isEnabledByDefault: true,
                description: Description_009AOR
                );

        #endregion EOS Add Or Remove Listener Diagnostic

    }
}