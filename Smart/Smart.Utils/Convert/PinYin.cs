using System.Text;

namespace Smart.Core.Utils
{
    public class PinYin
    {
        /// <summary>
        ///  »°ЇЇ„÷ „„÷ƒЄ
        /// </summary>
        /// <param name="hz"></param>
        /// <returns></returns>
        public string GetFirstLetter(string hz)
        {
            string ls_second_eng = "CJWGNSPGCGNESYPBTYYZDXYKYGTDJNNJQMBSGZSCYJSYYQPGKBZGYCYWJKGKLJSWKPJQHYTWDDZLSGMRYPYWWCCKZNKYDGTTNGJEYKKZYTCJNMCYLQLYPYQFQRPZSLWBTGKJFYXJWZLTBNCXJJJJZXDTTSQZYCDXXHGCKBPHFFSSWYBGMXLPBYLLLHLXSPZMYJHSOJNGHDZQYKLGJHSGQZHXQGKEZZWYSCSCJXYEYXADZPMDSSMZJZQJYZCDJZWQJBDZBXGZNZCPWHKXHQKMWFBPBYDTJZZKQHYLYGXFPTYJYYZPSZLFCHMQSHGMXXSXJJSDCSBBQBEFSJYHWWGZKPYLQBGLDLCCTNMAYDDKSSNGYCSGXLYZAYBNPTSDKDYLHGYMYLCXPYCJNDQJWXQXFYYFJLEJBZRXCCQWQQSBNKYMGPLBMJRQCFLNYMYQMSQTRBCJTHZTQFRXQ" +
         "HXMJJCJLXQGJMSHZKBSWYEMYLTXFSYDSGLYCJQXSJNQBSCTYHBFTDCYZDJWYGHQFRXWCKQKXEBPTLPXJZSRMEBWHJLBJSLYYSMDXLCLQKXLHXJRZJMFQHXHWYWSBHTRXXGLHQHFNMNYKLDYXZPWLGGTMTCFPAJJZYLJTYANJGBJPLQGDZYQYAXBKYSECJSZNSLYZHZXLZCGHPXZHZNYTDSBCJKDLZAYFMYDLEBBGQYZKXGLDNDNYSKJSHDLYXBCGHXYPKDJMMZNGMMCLGWZSZXZJFZNMLZZTHCSYDBDLLSCDDNLKJYKJSYCJLKOHQASDKNHCSGANHDAASHTCPLCPQYBSDMPJLPCJOQLCDHJJYSPRCHNWJNLHLYYQYYWZPTCZGWWMZFFJQQQQYXACLBHKDJXDGMMYDJXZLLSYGXGKJRYWZWYCLZMSSJZLDBYDCFCXYHLXCHYZJQSFQAGMNYXPFRKSSB" +
         "JLYXYSYGLNSCMHCWWMNZJJLXXHCHSYDSTTXRYCYXBYHCSMXJSZNPWGPXXTAYBGAJCXLYSDCCWZOCWKCCSBNHCPDYZNFCYYTYCKXKYBSQKKYTQQXFCWCHCYKELZQBSQYJQCCLMTHSYWHMKTLKJLYCXWHEQQHTQHZPQSQSCFYMMDMGBWHWLGSSLYSDLMLXPTHMJHWLJZYHZJXHTXJLHXRSWLWZJCBXMHZQXSDZPMGFCSGLSXYMJSHXPJXWMYQKSMYPLRTHBXFTPMHYXLCHLHLZYLXGSSSSTCLSLDCLRPBHZHXYYFHBBGDMYCNQQWLQHJJZYWJZYEJJDHPBLQXTQKWHLCHQXAGTLXLJXMSLXHTZKZJECXJCJNMFBYCSFYWYBJZGNYSDZSQYRSLJPCLPWXSDWEJBJCBCNAYTWGMPAPCLYQPCLZXSBNMSGGFNZJJBZSFZYNDXHPLQKZCZWALSBCCJXJYZGWKYP" +
         "SGXFZFCDKHJGXDLQFSGDSLQWZKXTMHSBGZMJZRGLYJBPMLMSXLZJQQHZYJCZYDJWBMJKLDDPMJEGXYHYLXHLQYQHKYCWCJMYYXNATJHYCCXZPCQLBZWWYTWBQCMLPMYRJCCCXFPZNZZLJPLXXYZTZLGDLDCKLYRZZGQTGJHHHJLJAXFGFJZSLCFDQZLCLGJDJCSNCLLJPJQDCCLCJXMYZFTSXGCGSBRZXJQQCTZHGYQTJQQLZXJYLYLBCYAMCSTYLPDJBYREGKLZYZHLYSZQLZNWCZCLLWJQJJJKDGJZOLBBZPPGLGHTGZXYGHZMYCNQSYCYHBHGXKAMTXYXNBSKYZZGJZLQJDFCJXDYGJQJJPMGWGJJJPKQSBGBMMCJSSCLPQPDXCDYYKYFCJDDYYGYWRHJRTGZNYQLDKLJSZZGZQZJGDYKSHPZMTLCPWNJAFYZDJCNMWESCYGLBTZCGMSSLLYXQSXSBSJS" +
         "BBSGGHFJLWPMZJNLYYWDQSHZXTYYWHMCYHYWDBXBTLMSYYYFSXJCSDXXLHJHFSSXZQHFZMZCZTQCXZXRTTDJHNNYZQQMNQDMMGYYDXMJGDHCDYZBFFALLZTDLTFXMXQZDNGWQDBDCZJDXBZGSQQDDJCMBKZFFXMKDMDSYYSZCMLJDSYNSPRSKMKMPCKLGDBQTFZSWTFGGLYPLLJZHGJJGYPZLTCSMCNBTJBQFKTHBYZGKPBBYMTTSSXTBNPDKLEYCJNYCDYKZDDHQHSDZSCTARLLTKZLGECLLKJLQJAQNBDKKGHPJTZQKSECSHALQFMMGJNLYJBBTMLYZXDCJPLDLPCQDHZYCBZSCZBZMSLJFLKRZJSNFRGJHXPDHYJYBZGDLQCSEZGXLBLGYXTWMABCHECMWYJYZLLJJYHLGBDJLSLYGKDZPZXJYYZLWCXSZFGWYYDLYHCLJSCMBJHBLYZLYCBLYDPDQYSXQZB" +
         "YTDKYXJYYCNRJMDJGKLCLJBCTBJDDBBLBLCZQRPXJCGLZCSHLTOLJNMDDDLNGKAQHQHJGYKHEZNMSHRPHQQJCHGMFPRXHJGDYCHGHLYRZQLCYQJNZSQTKQJYMSZSWLCFQQQXYFGGYPTQWLMCRNFKKFSYYLQBMQAMMMYXCTPSHCPTXXZZSMPHPSHMCLMLDQFYQXSZYJDJJZZHQPDSZGLSTJBCKBXYQZJSGPSXQZQZRQTBDKYXZKHHGFLBCSMDLDGDZDBLZYYCXNNCSYBZBFGLZZXSWMSCCMQNJQSBDQSJTXXMBLTXZCLZSHZCXRQJGJYLXZFJPHYMZQQYDFQJJLZZNZJCDGZYGCTXMZYSCTLKPHTXHTLBJXJLXSCDQXCBBTJFQZFSLTJBTKQBXXJJLJCHCZDBZJDCZJDCPRNPQCJPFCZLCLZXZDMXMPHJSGZGSZZQJYLWTJPFSYASMCJBTZKYCWMYTCSJJLJCQLWZM" +
         "ALBXYFBPNLSFHTGJWEJJXXGLLJSTGSHJQLZFKCGNNDSZFDEQFHBSAQTGLLBXMMYGSZLDYDQMJJRGBJTKGDHGKBLQKBDMBYLXWCXYTTYBKMRTJZXQJBHLMHMJJZMQASLDCYXYQDLQCAFYWYXQHZ";

            string ls_second_ch = "Ў°ЎҐЎ£Ў§Ў•Ў¶ЎІЎ®Ў©Ў™ЎЂЎђЎ≠ЎЃЎѓЎ∞Ў±Ў≤Ў≥ЎіЎµЎґЎЈЎЄЎєЎЇЎїЎЉЎљ" +
         "ЎЊЎњЎјЎЅЎ¬Ў√ЎƒЎ≈Ў∆Ў«Ў»Ў…Ў ЎЋЎћЎЌЎќЎѕЎ–Ў—Ў“Ў”Ў‘Ў’Ў÷Ў„ЎЎЎўЎЏЎџЎ№ЎЁЎёЎяЎаЎбЎвЎгЎдЎеЎжЎзЎиЎйЎкЎлЎмЎнЎоЎпЎрЎсЎтЎуЎфЎхЎцЎчЎшЎщЎъЎыЎьЎэЎюў°ўҐў£ў§ў•ў¶ўІў®ў©ў™ўЂўђў≠ўЃўѓў∞ў±ў≤ў≥ўіўµўґўЈўЄўєўЇўїўЉўљўЊўњўјўЅў¬ў√ўƒў≈ў∆ў«ў»ў…ў ўЋўћўЌўќўѕў–ў—ў“ў”ў‘ў’ў÷ў„ўЎўўўЏўџў№ўЁўёўяўаўбўвўгўдўеўжўзўиўйўкўлўмўнўоўпўрўсўтўуўфўхўцўчўшўщўъўыўьўэўюЏ°ЏҐЏ£Џ§Џ•Џ¶ЏІЏ®Џ©Џ™ЏЂЏђЏ≠ЏЃЏѓЏ∞Џ±Џ≤Џ≥ЏіЏµЏґЏЈЏЄЏєЏЇЏїЏЉЏљЏЊЏњЏјЏЅЏ¬Џ√ЏƒЏ≈Џ∆Џ«Џ»Џ…Џ ЏЋЏћЏЌЏќЏѕЏ–Џ—Џ“Џ”Џ‘Џ’Џ÷Џ„ЏЎЏўЏЏЏџЏ№ЏЁЏёЏяЏаЏбЏвЏгЏдЏеЏжЏзЏи" +
         "ЏйЏкЏлЏмЏнЏоЏпЏрЏсЏтЏуЏфЏхЏцЏчЏшЏщЏъЏыЏьЏэЏюџ°џҐџ£џ§џ•џ¶џІџ®џ©џ™џЂџђџ≠џЃџѓџ∞џ±џ≤џ≥џіџµџґџЈџЄџєџЇџїџЉџљџЊџњџјџЅџ¬џ√џƒџ≈џ∆џ«џ»џ…џ џЋџћџЌџќџѕџ–џ—џ“џ”џ‘џ’џ÷џ„џЎџўџЏџџџ№џЁџёџяџаџбџвџгџдџеџжџзџиџйџкџлџмџнџоџпџрџсџтџуџфџхџцџчџшџщџъџыџьџэџю№°№Ґ№£№§№•№¶№І№®№©№™№Ђ№ђ№≠№Ѓ№ѓ№∞№±№≤№≥№і№µ№ґ№Ј№Є№є№Ї№ї№Љ№љ№Њ№њ№ј№Ѕ№¬№√№ƒ№≈№∆№«№»№…№ №Ћ№ћ№Ќ№ќ№ѕ№–№—№“№”№‘№’№÷№„№Ў№ў№Џ№џ№№№Ё№ё№я№а№б№в№г№д№е№ж№з№и№й№к№л№м№н№о№п№р№с№т№у№ф№х№ц№ч№ш№щ№ъ№ы№ь№э№юЁ°ЁҐЁ£Ё§Ё•Ё¶ЁІЁ®Ё©Ё™ЁЂЁђЁ≠ЁЃЁѓЁ∞Ё±Ё≤Ё≥ЁіЁµЁґ" +
         "ЁЈЁЄЁєЁЇЁїЁЉЁљЁЊЁњЁјЁЅЁ¬Ё√ЁƒЁ≈Ё∆Ё«Ё»Ё…Ё ЁЋЁћЁЌЁќЁѕЁ–Ё—Ё“Ё”Ё‘Ё’Ё÷Ё„ЁЎЁўЁЏЁџЁ№ЁЁЁёЁяЁаЁбЁвЁгЁдЁеЁжЁзЁиЁйЁкЁлЁмЁнЁоЁпЁрЁсЁтЁуЁфЁхЁцЁчЁшЁщЁъЁыЁьЁэЁюё°ёҐё£ё§ё•ё¶ёІё®ё©ё™ёЂёђё≠ёЃёѓё∞ё±ё≤ё≥ёіёµёґёЈёЄёєёЇёїёЉёљёЊёњёјёЅё¬ё√ёƒё≈ё∆ё«ё»ё…ё ёЋёћёЌёќёѕё–ё—ё“ё”ё‘ё’ё÷ё„ёЎёўёЏёџё№ёЁёёёяёаёбёвёгёдёеёжёзёиёйёкёлёмёнёоёпёрёсётёуёфёхёцёчёшёщёъёыёьёэёюя°яҐя£я§я•я¶яІя®я©я™яЂяђя≠яЃяѓя∞я±я≤я≥яіяµяґяЈяЄяєяЇяїяЉяљяЊяњяјяЅя¬я√яƒя≈я∆я«я»я…я яЋяћяЌяќяѕя–я—я“я”я‘я’я÷я„яЎяўяЏяџя№яЁяёяяяаябявяг" +
         "ядяеяжязяияйякялямяняояпярясятяуяфяхяцячяшящяъяыяьяэяюа°аҐа£а§а•а¶аІа®а©а™аЂађа≠аЃаѓа∞а±а≤а≥аіаµаґаЈаЄаєаЇаїаЉаљаЊањајаЅа¬а√аƒа≈а∆а«а»а…а аЋаћаЌаќаѕа–а—а“а”а‘а’а÷а„аЎаўаЏаџа№аЁаёаяааабавагадаеажазаиайакаламанаоапарасатауафахацачашащаъаыаьаэаюб°бҐб£б§б•б¶бІб®б©б™бЂбђб≠бЃбѓб∞б±б≤б≥бібµбґбЈбЄбєбЇбїбЉбљбЊбњбјбЅб¬б√бƒб≈б∆б«б»б…б бЋбћбЌбќбѕб–б—б“б”б‘б’б÷б„бЎбўбЏбџб№бЁбёбябабббвбгбдбебжбзбибйбкблбмбнбобпбрбсбтбубфбхбцбчбшбщбъбыбьбэбюв°вҐв£в§в•в¶вІв®в©в™вЂвђв≠вЃвѓв∞в±в≤в≥вівµ" +
         "вґвЈвЄвєвЇвївЉвљвЊвњвјвЅв¬в√вƒв≈в∆в«в»в…в вЋвћвЌвќвѕв–в—в“в”в‘в’в÷в„вЎвўвЏвџв№вЁвёвявавбвввгвдвевжвзвивйвквлвмвнвовпврвсвтвувфвхвцвчвшвщвъвывьвэвюг°гҐг£г§г•г¶гІг®г©г™гЂгђг≠гЃгѓг∞г±г≤г≥гігµгґгЈгЄгєгЇгїгЉгљгЊгњгјгЅг¬г√гƒг≈г∆г«г»г…г гЋгћгЌгќгѕг–г—г“г”г‘г’г÷г„гЎгўгЏгџг№гЁгёгягагбгвгггдгегжгзгигйгкглгмгнгогпгргсгтгугфгхгцгчгшгщгъгыгьгэгюд°дҐд£д§д•д¶дІд®д©д™дЂдђд≠дЃдѓд∞д±д≤д≥дідµдґдЈдЄдєдЇдїдЉдљдЊдњдјдЅд¬д√дƒд≈д∆д«д»д…д дЋдћдЌдќдѕд–д—д“д”д‘д’д÷д„дЎдўдЏдџд№дЁдёдядадбдвдгдддедждзди" +
         "дйдкдлдмдндодпдрдсдтдудфдхдцдчдшдщдъдыдьдэдюе°еҐе£е§е•е¶еІе®е©е™еЂеђе≠еЃеѓе∞е±е≤е≥еіеµеґеЈеЄеєеЇеїеЉељеЊењејеЅе¬е√еƒе≈е∆е«е»е…е еЋећеЌеќеѕе–е—е“е”е‘е’е÷е„еЎеўеЏеџе№еЁеёеяеаебевегедееежезеиейекелеменеоепересетеуефехецечешещеъеыеьеэеюж°жҐж£ж§ж•ж¶жІж®ж©ж™жЂжђж≠жЃжѓж∞ж±ж≤ж≥жіжµжґжЈжЄжєжЇжїжЉжљжЊжњжјжЅж¬ж√жƒж≈ж∆ж«ж»ж…ж жЋжћжЌжќжѕж–ж—ж“ж”ж‘ж’ж÷ж„жЎжўжЏжџж№жЁжёжяжажбжвжгжджежжжзжижйжкжлжмжнжожпжржсжтжужфжхжцжчжшжщжъжыжьжэжюз°зҐз£з§з•з¶зІз®з©з™зЂзђз≠зЃзѓз∞з±з≤з≥зізµзґзЈзЄзєзЇзїзЉзљ" +
         "зЊзњзјзЅз¬з√зƒз≈з∆з«з»з…з зЋзћзЌзќзѕз–з—з“з”з‘з’з÷з„зЎзўзЏзџз№зЁзёзязазбзвзгздзезжзззизйзкзлзмзнзозпзрзсзтзузфзхзцзчзшзщзъзызьзэзюи°иҐи£и§и•и¶иІи®и©и™иЂиђи≠иЃиѓи∞и±и≤и≥иіиµиґиЈиЄиєиЇиїиЉиљиЊињијиЅи¬и√иƒи≈и∆и«и»и…и иЋићиЌиќиѕи–и—и“и”и‘и’и÷и„иЎиўиЏиџи№иЁиёияиаибивигидиеижизииийикилиминиоипириситиуифихицичишищиъиыиьиэиюй°йҐй£й§й•й¶йІй®й©й™йЂйђй≠йЃйѓй∞й±й≤й≥йійµйґйЈйЄйєйЇйїйЉйљйЊйњйјйЅй¬й√йƒй≈й∆й«й»й…й йЋйћйЌйќйѕй–й—й“й”й‘й’й÷й„йЎйўйЏйџй№йЁйёйяйайбйвйгйдйейжйзйийййкйлймйнйойпйрйсйтйу" +
         "йфйхйцйчйшйщйъйыйьйэйюк°кҐк£к§к•к¶кІк®к©к™кЂкђк≠кЃкѓк∞к±к≤к≥кікµкґкЈкЄкєкЇкїкЉкљкЊкњкјкЅк¬к√кƒк≈к∆к«к»к…к кЋкћкЌкќкѕк–к—к“к”к‘к’к÷к„кЎкўкЏкџк№кЁкёкякакбквкгкдкекжкзкикйккклкмкнкокпкркскткукфкхкцкчкшкщкъкыкькэкюл°лҐл£л§л•л¶лІл®л©л™лЂлђл≠лЃлѓл∞л±л≤л≥лілµлґлЈлЄлєлЇлїлЉлљлЊлњлјлЅл¬л√лƒл≈л∆л«л»л…л лЋлћлЌлќлѕл–л—л“л”л‘л’л÷л„лЎлўлЏлџл№лЁлёлялалблвлглдлелжлзлилйлклллмлнлолплрлслтлулфлхлцлчлшлщлълыльлэлюм°мҐм£м§м•м¶мІм®м©м™мЂмђм≠мЃмѓм∞м±м≤м≥мімµмґмЈмЄмємЇмїмЉмљмЊмњмјмЅм¬м√мƒм≈м∆м«м»м…м мЋмћмЌ" +
         "мќмѕм–м—м“м”м‘м’м÷м„мЎмўмЏмџм№мЁмёмямамбмвмгмдмемжмзмимймкмлмммнмомпмрмсмтмумфмхмцмчмшмщмъмымьмэмюн°нҐн£н§н•н¶нІн®н©н™нЂнђн≠нЃнѓн∞н±н≤н≥нінµнґнЈнЄнєнЇнїнЉнљнЊнњнјнЅн¬н√нƒн≈н∆н«н»н…н нЋнћнЌнќнѕн–н—н“н”н‘н’н÷н„нЎнўнЏнџн№нЁнёнянанбнвнгндненжнзнинйнкнлнмнннонпнрнснтнунфнхнцнчншнщнъныньнэнюо°оҐо£о§о•о¶оІо®о©о™оЂођо≠оЃоѓо∞о±о≤о≥оіоµоґоЈоЄоєоЇоїоЉољоЊоњојоЅо¬о√оƒо≈о∆о«о»о…о оЋоћоЌоќоѕо–о—о“о”о‘о’о÷о„оЎоўоЏоџо№оЁоёояоаобовогодоеожозоиойоколомонооопоросотоуофохоцочошощоъоыоьоэоюп°пҐп£п§п•п¶пІп®п©п™" +
         "пЂпђп≠пЃпѓп∞п±п≤п≥піпµпґпЈпЄпєпЇпїпЉпљпЊпњпјпЅп¬п√пƒп≈п∆п«п»п…п пЋпћпЌпќпѕп–п—п“п”п‘п’п÷п„пЎпўпЏпџп№пЁпёпяпапбпвпгпдпепжпзпипйпкплпмпнпопппрпсптпупфпхпцпчпшпщпъпыпьпэпюр°рҐр£р§р•р¶рІр®р©р™рЂрђр≠рЃрѓр∞р±р≤р≥рірµрґрЈрЄрєрЇрїрЉрљрЊрњрјрЅр¬р√рƒр≈р∆р«р»р…р рЋрћрЌрќрѕр–р—р“р”р‘р’р÷р„рЎрўрЏрџр№рЁрёрярарбрвргрдрержрзрирйркрлрмрнрорпрррсртрурфрхрцрчршрщрърырьрэрюс°сҐс£с§с•с¶сІс®с©с™сЂсђс≠сЃсѓс∞с±с≤с≥сісµсґсЈсЄсєсЇсїсЉсљсЊсњсјсЅс¬с√сƒс≈с∆с«с…с сЋсћсЌсќсѕс–с—с“с”с‘с’с÷с„сЎсўсЏсџс№сЁсёсясасвсгсдсесжсз" +
         "сисйскслсмснсоспсрссстсусфсхсцсчсшсщсъсысьсэсют°тҐт£т§т•т¶тІт®т©т™тЂтђт≠тЃтѓт∞т±т≤т≥тітµтґтЈтЄтєтЇтїтЉтљтЊтњтјтЅт¬т√тƒт≈т∆т«т»т…т тЋтћтЌтќтѕт–т—т“т”т‘т’т÷т„тЎтўтЏтџт№тЁтётятатбтвтгтдтетжтзтитйтктлтмтнтотптртстттутфтхтцтчтштщтътытьтэтюу°уҐу£у§у•у¶уІу®у©у™уЂуђу≠уЃуѓу∞у±у≤у≥уіуµуґуЈуЄуєуЇуїуЉуљуЊуњујуЅу¬у√уƒу≈у∆у«у»у…у уЋућуЌуќуѕу–у—у“у”у‘у’у÷у„уЎуўуЏуџу№уЁуёуяуаубувугудуеужузуиуйукулумунуоупурусутуууфухуцучушущуъуыуьуэуюф°фҐф£ф§ф•ф¶фІф®ф©ф™фЂфђф≠фЃфѓф∞ф±ф≤ф≥фіфµфґфЈфЄфєфЇфїфЉфљфЊфњфјфЅф¬ф√фƒф≈ф∆ф«" +
         "ф»ф…ф фЋфћфЌфќфѕф–ф—ф“ф”ф‘ф’ф÷ф„фЎфўфЏфџф№фЁфёфяфафбфвфгфдфефжфзфифйфкфлфмфнфофпфрфсфтфуфффхфцфчфшфщфъфыфьфэфюх°хҐх£х§х•х¶хІх®х©х™хЂхђх≠хЃхѓх∞х±х≤х≥хіхµхґхЈхЄхєхЇхїхЉхљхЊхњхјхЅх¬х√хƒх≈х∆х«х»х…х хЋхћхЌхќхѕх–х—х“х”х‘х’х÷х„хЎхўхЏхџх№хЁхёхяхахбхвхгхдхехжхзхихйхкхлхмхнхохпхрхсхтхухфхххцхчхшхщхъхыхьхэхюц°цҐц£ц§ц•ц¶цІц®ц©ц™цЂцђц≠цЃцѓц∞ц±ц≤ц≥ціцµцґцЈцЄцєцЇцїцЉцљцЊцњцјцЅц¬ц√цƒц≈ц∆ц«ц»ц…ц цЋцћцЌцќцѕц–ц—ц“ц”ц‘ц’ц÷ц„цЎцўцЏцџц№цЁцёцяцацбцвцгцдцецжцзцицйцкцлцмцнцоцпцрцсцтцуцфцхцццчцшцщцъцыцьцэцюч°чҐч£ч§ч•ч¶чІ" +
         "ч®ч©ч™чЂчђч≠чЃчѓч∞ч±ч≤ч≥чічµчґчЈчЄчєчЇчїчЉчљчЊчњчјчЅч¬ч√чƒч≈ч∆ч«ч»ч…ч чЋчћчЌчќчѕч–ч—ч“ч”ч‘ч’ч÷ч„чЎчўчЏчџч№чЁчёчячачбчвчгчдчечжчзчичйчкчлчмчнчочпчрчсчтчучфчхчцчччшчщчъчычьчэчю";

            string return_py = "";
            byte[] array = new byte[2];

            for (int i = 0; i < hz.Length; i++)
            {
                array = System.Text.Encoding.Default.GetBytes(hz[i].ToString());

                //Ј«ЇЇ„÷
                if (array[0] < 176)
                {
                    return_py += hz[i];
                }
                //“їЉґЇЇ„÷
                else if (array[0] >= 176 && array[0] <= 215)
                {

                    if (hz[i].ToString().CompareTo("‘—") >= 0)
                        return_py += "z";
                    else if (hz[i].ToString().CompareTo("—є") >= 0)
                        return_py += "y";
                    else if (hz[i].ToString().CompareTo("ќф") >= 0)
                        return_py += "x";
                    else if (hz[i].ToString().CompareTo("Ќџ") >= 0)
                        return_py += "w";
                    else if (hz[i].ToString().CompareTo("Ћъ") >= 0)
                        return_py += "t";
                    else if (hz[i].ToString().CompareTo("»ц") >= 0)
                        return_py += "s";
                    else if (hz[i].ToString().CompareTo("»ї") >= 0)
                        return_py += "r";
                    else if (hz[i].ToString().CompareTo("∆Џ") >= 0)
                        return_py += "q";
                    else if (hz[i].ToString().CompareTo("≈Њ") >= 0)
                        return_py += "p";
                    else if (hz[i].ToString().CompareTo("≈ґ") >= 0)
                        return_py += "o";
                    else if (hz[i].ToString().CompareTo("ƒ√") >= 0)
                        return_py += "n";
                    else if (hz[i].ToString().CompareTo("¬и") >= 0)
                        return_py += "m";
                    else if (hz[i].ToString().CompareTo("јђ") >= 0)
                        return_py += "l";
                    else if (hz[i].ToString().CompareTo("њ¶") >= 0)
                        return_py += "k";
                    else if (hz[i].ToString().CompareTo("їч") >= 0)
                        return_py += "j";
                    else if (hz[i].ToString().CompareTo("єю") >= 0)
                        return_py += "h";
                    else if (hz[i].ToString().CompareTo("ЄЅ") >= 0)
                        return_py += "g";
                    else if (hz[i].ToString().CompareTo("ЈҐ") >= 0)
                        return_py += "f";
                    else if (hz[i].ToString().CompareTo("ґк") >= 0)
                        return_py += "e";
                    else if (hz[i].ToString().CompareTo("іо") >= 0)
                        return_py += "d";
                    else if (hz[i].ToString().CompareTo("≤Ѕ") >= 0)
                        return_py += "c";
                    else if (hz[i].ToString().CompareTo("∞≈") >= 0)
                        return_py += "b";
                    else if (hz[i].ToString().CompareTo("∞°") >= 0)
                        return_py += "a";
                }
                //ґюЉґЇЇ„÷
                else if (array[0] >= 215)
                {
                    return_py += ls_second_eng.Substring(ls_second_ch.IndexOf(hz[i].ToString(), 0), 1);
                }
            }
            return return_py.ToUpper();
        }

        /// <summary>
        /// »°ЇЇ„÷∆і“фµƒ „„÷ƒЄ
        /// </summary>
        /// <param name="UnName">ЇЇ„÷</param>
        /// <returns> „„÷ƒЄ</returns>
        public static string GetCodstring(string UnName)
        {
            int i = 0;
            ushort key = 0;
            string strResult = string.Empty;

            Encoding unicode = Encoding.Unicode;
            Encoding gbk = Encoding.GetEncoding(936);
            byte[] unicodeBytes = unicode.GetBytes(UnName);
            byte[] gbkBytes = Encoding.Convert(unicode, gbk, unicodeBytes);
            while (i < gbkBytes.Length)
            {
                if (gbkBytes[i] <= 127)
                { 
                    strResult = strResult + (char)gbkBytes[i];
                    i++;
                }
                #region …ъ≥…ЇЇ„÷∆і“фЉт¬л,»°∆і“ф „„÷ƒЄ
                else
                {
                    key = (ushort)(gbkBytes[i] * 256 + gbkBytes[i + 1]);
                    if (key >= '\uB0A1' && key <= '\uB0C4')
                    {
                        strResult = strResult + "A";
                    }
                    else if (key >= '\uB0C5' && key <= '\uB2C0')
                    {
                        strResult = strResult + "B";
                    }
                    else if (key >= '\uB2C1' && key <= '\uB4ED')
                    {
                        strResult = strResult + "C";
                    }
                    else if (key >= '\uB4EE' && key <= '\uB6E9')
                    {
                        strResult = strResult + "D";
                    }
                    else if (key >= '\uB6EA' && key <= '\uB7A1')
                    {
                        strResult = strResult + "E";
                    }
                    else if (key >= '\uB7A2' && key <= '\uB8C0')
                    {
                        strResult = strResult + "F";
                    }
                    else if (key >= '\uB8C1' && key <= '\uB9FD')
                    {
                        strResult = strResult + "G";
                    }
                    else if (key >= '\uB9FE' && key <= '\uBBF6')
                    {
                        strResult = strResult + "H";
                    }
                    else if (key >= '\uBBF7' && key <= '\uBFA5')
                    {
                        strResult = strResult + "J";
                    }
                    else if (key >= '\uBFA6' && key <= '\uC0AB')
                    {
                        strResult = strResult + "K";
                    }
                    else if (key >= '\uC0AC' && key <= '\uC2E7')
                    {
                        strResult = strResult + "L";
                    }
                    else if (key >= '\uC2E8' && key <= '\uC4C2')
                    {
                        strResult = strResult + "M";
                    }
                    else if (key >= '\uC4C3' && key <= '\uC5B5')
                    {
                        strResult = strResult + "N";
                    }
                    else if (key >= '\uC5B6' && key <= '\uC5BD')
                    {
                        strResult = strResult + "O";
                    }
                    else if (key >= '\uC5BE' && key <= '\uC6D9')
                    {
                        strResult = strResult + "P";
                    }
                    else if (key >= '\uC6DA' && key <= '\uC8BA')
                    {
                        strResult = strResult + "Q";
                    }
                    else if (key >= '\uC8BB' && key <= '\uC8F5')
                    {
                        strResult = strResult + "R";
                    }
                    else if (key >= '\uC8F6' && key <= '\uCBF9')
                    {
                        strResult = strResult + "S";
                    }
                    else if (key >= '\uCBFA' && key <= '\uCDD9')
                    {
                        strResult = strResult + "T";
                    }
                    else if (key >= '\uCDDA' && key <= '\uCEF3')
                    {
                        strResult = strResult + "W";
                    }
                    else if (key >= '\uCEF4' && key <= '\uD188')
                    {
                        strResult = strResult + "X";
                    }
                    else if (key >= '\uD1B9' && key <= '\uD4D0')
                    {
                        strResult = strResult + "Y";
                    }
                    else if (key >= '\uD4D1' && key <= '\uD7F9')
                    {
                        strResult = strResult + "Z";
                    }
                    else
                    {
                        strResult = strResult + "?";
                    }
                    i = i + 2;
                }
                #endregion
            }
            return strResult;
        }

        /// <summary>
        /// їс»°÷–ќƒ»Ђ∆і
        /// </summary>
        public static string GetQuanPin( string x )
        {

            int[] iA = new int[]

             {

                 -20319 ,-20317 ,-20304 ,-20295 ,-20292 ,-20283 ,-20265 ,-20257 ,-20242 ,-20230

                 ,-20051 ,-20036 ,-20032 ,-20026 ,-20002 ,-19990 ,-19986 ,-19982 ,-19976 ,-19805

                 ,-19784 ,-19775 ,-19774 ,-19763 ,-19756 ,-19751 ,-19746 ,-19741 ,-19739 ,-19728

                 ,-19725 ,-19715 ,-19540 ,-19531 ,-19525 ,-19515 ,-19500 ,-19484 ,-19479 ,-19467

                 ,-19289 ,-19288 ,-19281 ,-19275 ,-19270 ,-19263 ,-19261 ,-19249 ,-19243 ,-19242

                 ,-19238 ,-19235 ,-19227 ,-19224 ,-19218 ,-19212 ,-19038 ,-19023 ,-19018 ,-19006

                 ,-19003 ,-18996 ,-18977 ,-18961 ,-18952 ,-18783 ,-18774 ,-18773 ,-18763 ,-18756

                 ,-18741 ,-18735 ,-18731 ,-18722 ,-18710 ,-18697 ,-18696 ,-18526 ,-18518 ,-18501

                 ,-18490 ,-18478 ,-18463 ,-18448 ,-18447 ,-18446 ,-18239 ,-18237 ,-18231 ,-18220

                 ,-18211 ,-18201 ,-18184 ,-18183 ,-18181 ,-18012 ,-17997 ,-17988 ,-17970 ,-17964

                 ,-17961 ,-17950 ,-17947 ,-17931 ,-17928 ,-17922 ,-17759 ,-17752 ,-17733 ,-17730

                 ,-17721 ,-17703 ,-17701 ,-17697 ,-17692 ,-17683 ,-17676 ,-17496 ,-17487 ,-17482

                 ,-17468 ,-17454 ,-17433 ,-17427 ,-17417 ,-17202 ,-17185 ,-16983 ,-16970 ,-16942

                 ,-16915 ,-16733 ,-16708 ,-16706 ,-16689 ,-16664 ,-16657 ,-16647 ,-16474 ,-16470

                 ,-16465 ,-16459 ,-16452 ,-16448 ,-16433 ,-16429 ,-16427 ,-16423 ,-16419 ,-16412

                 ,-16407 ,-16403 ,-16401 ,-16393 ,-16220 ,-16216 ,-16212 ,-16205 ,-16202 ,-16187

                 ,-16180 ,-16171 ,-16169 ,-16158 ,-16155 ,-15959 ,-15958 ,-15944 ,-15933 ,-15920

                 ,-15915 ,-15903 ,-15889 ,-15878 ,-15707 ,-15701 ,-15681 ,-15667 ,-15661 ,-15659

                 ,-15652 ,-15640 ,-15631 ,-15625 ,-15454 ,-15448 ,-15436 ,-15435 ,-15419 ,-15416

                 ,-15408 ,-15394 ,-15385 ,-15377 ,-15375 ,-15369 ,-15363 ,-15362 ,-15183 ,-15180

                 ,-15165 ,-15158 ,-15153 ,-15150 ,-15149 ,-15144 ,-15143 ,-15141 ,-15140 ,-15139

                 ,-15128 ,-15121 ,-15119 ,-15117 ,-15110 ,-15109 ,-14941 ,-14937 ,-14933 ,-14930

                 ,-14929 ,-14928 ,-14926 ,-14922 ,-14921 ,-14914 ,-14908 ,-14902 ,-14894 ,-14889

                 ,-14882 ,-14873 ,-14871 ,-14857 ,-14678 ,-14674 ,-14670 ,-14668 ,-14663 ,-14654

                 ,-14645 ,-14630 ,-14594 ,-14429 ,-14407 ,-14399 ,-14384 ,-14379 ,-14368 ,-14355

                 ,-14353 ,-14345 ,-14170 ,-14159 ,-14151 ,-14149 ,-14145 ,-14140 ,-14137 ,-14135

                 ,-14125 ,-14123 ,-14122 ,-14112 ,-14109 ,-14099 ,-14097 ,-14094 ,-14092 ,-14090

                 ,-14087 ,-14083 ,-13917 ,-13914 ,-13910 ,-13907 ,-13906 ,-13905 ,-13896 ,-13894

                 ,-13878 ,-13870 ,-13859 ,-13847 ,-13831 ,-13658 ,-13611 ,-13601 ,-13406 ,-13404

                 ,-13400 ,-13398 ,-13395 ,-13391 ,-13387 ,-13383 ,-13367 ,-13359 ,-13356 ,-13343

                 ,-13340 ,-13329 ,-13326 ,-13318 ,-13147 ,-13138 ,-13120 ,-13107 ,-13096 ,-13095

                 ,-13091 ,-13076 ,-13068 ,-13063 ,-13060 ,-12888 ,-12875 ,-12871 ,-12860 ,-12858

                 ,-12852 ,-12849 ,-12838 ,-12831 ,-12829 ,-12812 ,-12802 ,-12607 ,-12597 ,-12594

                 ,-12585 ,-12556 ,-12359 ,-12346 ,-12320 ,-12300 ,-12120 ,-12099 ,-12089 ,-12074

                 ,-12067 ,-12058 ,-12039 ,-11867 ,-11861 ,-11847 ,-11831 ,-11798 ,-11781 ,-11604

                 ,-11589 ,-11536 ,-11358 ,-11340 ,-11339 ,-11324 ,-11303 ,-11097 ,-11077 ,-11067

                 ,-11055 ,-11052 ,-11045 ,-11041 ,-11038 ,-11024 ,-11020 ,-11019 ,-11018 ,-11014

                 ,-10838 ,-10832 ,-10815 ,-10800 ,-10790 ,-10780 ,-10764 ,-10587 ,-10544 ,-10533

                 ,-10519 ,-10331 ,-10329 ,-10328 ,-10322 ,-10315 ,-10309 ,-10307 ,-10296 ,-10281

                 ,-10274 ,-10270 ,-10262 ,-10260 ,-10256 ,-10254

             };

            string[] sA = new string[]
         {
             "a","ai","an","ang","ao"
             ,"ba","bai","ban","bang","bao","bei","ben","beng","bi","bian","biao","bie","bin"
             ,"bing","bo","bu"
             ,"ca","cai","can","cang","cao","ce","ceng","cha","chai","chan","chang","chao","che"
             ,"chen","cheng","chi","chong","chou","chu","chuai","chuan","chuang","chui","chun"
             ,"chuo","ci","cong","cou","cu","cuan","cui","cun","cuo"
             ,"da","dai","dan","dang","dao","de","deng","di","dian","diao","die","ding","diu"
             ,"dong","dou","du","duan","dui","dun","duo"
             ,"e","en","er"
             ,"fa","fan","fang","fei","fen","feng","fo","fou","fu"
             ,"ga","gai","gan","gang","gao","ge","gei","gen","geng","gong","gou","gu","gua","guai"
             ,"guan","guang","gui","gun","guo"
             ,"ha","hai","han","hang","hao","he","hei","hen","heng","hong","hou","hu","hua","huai"
             ,"huan","huang","hui","hun","huo"
             ,"ji","jia","jian","jiang","jiao","jie","jin","jing","jiong","jiu","ju","juan","jue"
             ,"jun"
             ,"ka","kai","kan","kang","kao","ke","ken","keng","kong","kou","ku","kua","kuai","kuan"
             ,"kuang","kui","kun","kuo"
             ,"la","lai","lan","lang","lao","le","lei","leng","li","lia","lian","liang","liao","lie"
             ,"lin","ling","liu","long","lou","lu","lv","luan","lue","lun","luo"
             ,"ma","mai","man","mang","mao","me","mei","men","meng","mi","mian","miao","mie","min"
             ,"ming","miu","mo","mou","mu"
             ,"na","nai","nan","nang","nao","ne","nei","nen","neng","ni","nian","niang","niao","nie"
             ,"nin","ning","niu","nong","nu","nv","nuan","nue","nuo"
             ,"o","ou"
             ,"pa","pai","pan","pang","pao","pei","pen","peng","pi","pian","piao","pie","pin","ping"
             ,"po","pu"
             ,"qi","qia","qian","qiang","qiao","qie","qin","qing","qiong","qiu","qu","quan","que"
             ,"qun"
             ,"ran","rang","rao","re","ren","reng","ri","rong","rou","ru","ruan","rui","run","ruo"
             ,"sa","sai","san","sang","sao","se","sen","seng","sha","shai","shan","shang","shao","she"
             ,"shen","sheng","shi","shou","shu","shua","shuai","shuan","shuang","shui","shun","shuo","si"
             ,"song","sou","su","suan","sui","sun","suo"
             ,"ta","tai","tan","tang","tao","te","teng","ti","tian","tiao","tie","ting","tong","tou","tu"
             ,"tuan","tui","tun","tuo"
             ,"wa","wai","wan","wang","wei","wen","weng","wo","wu"
             ,"xi","xia","xian","xiang","xiao","xie","xin","xing","xiong","xiu","xu","xuan","xue","xun"
             ,"ya","yan","yang","yao","ye","yi","yin","ying","yo","yong","you","yu","yuan","yue","yun"
             ,"za","zai","zan","zang","zao","ze","zei","zen","zeng","zha","zhai","zhan","zhang","zhao"
             ,"zhe","zhen","zheng","zhi","zhong","zhou","zhu","zhua","zhuai","zhuan","zhuang","zhui"
             ,"zhun","zhuo","zi","zong","zou","zu","zuan","zui","zun","zuo"
         };
            byte[] B = new byte[2];
            string s = "";
            char[] c = x.ToCharArray();
            for ( int j = 0 ; j < c.Length ; j++ )
            {
                B = System.Text.Encoding.Default.GetBytes( c[ j ].ToString() );
                if ( ( int )( B[ 0 ] ) <= 160 && ( int )( B[ 0 ] ) >= 0 )
                {
                    s += c[ j ];
                }
                else
                {
                    for ( int i = ( iA.Length - 1 ) ; i >= 0 ; i-- )
                    {
                        if ( iA[ i ] <= ( int )( B[ 0 ] ) * 256 + ( int )( B[ 1 ] ) - 65536 )
                        {
                            s += sA[ i ];
                            break;
                        }
                    }
                }
            }
            return s;
        }



    }
}