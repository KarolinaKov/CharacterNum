using MathNet.Numerics;

List<int> SJedna = new List<int>{1,0,0};
List<int> SeatrdDva = new List<int>{0,1,0};
List<int> STri = new List<int>{0,0,1};
List<List<int>> Matice = new List<List<int>>{SJedna,SDva,STri};
int CharakterCisloVerze = 3;
int PocetModulu = 3;
int Rekurze(int CharakterCislo, int[] FixniIndex= null)
{
    if (FixniIndex==null) //impliciti nastaveni delky arraye podle skalaru CharakterCislo z uzivatelskeho vstupu
    {
// defaultni hodnoty(PocetModulu+1) arraye  musi byt jine nez mozne hodnoty, jinak budou sum cykly nespravne 
//preskakovat, plus jedna protoze CharakterCislo nemuze byt vetsi nez pocet modulu
     FixniIndex =Enumerable.Repeat(PocetModulu+1,CharakterCislo).ToArray();
    }
    if (CharakterCislo == 1)
    {
        int sum = 0;
        for (int i = 0; i<PocetModulu;i++)// suma aktualniho "nejmensiho" kola ze vsech k sum
        {
             if (FixniIndex.Contains(i))
            {continue;}
            FixniIndex[FixniIndex.Length-1] = i;
            int produkt = 1;
            for (int j = 0; j<PocetModulu; j++)//produkt, prochazeni vsech sloupcu v matici
            {
                bool LogSoucin = false;
                
                for ( int l = 0; l < FixniIndex.Length; l++)// prochazeni fixnimi indexy z predeslych sum, resp radky 
                                                            // a provadeni logickeho soucinu
                {
                    bool LocalBool = false;
                   if (Matice[FixniIndex[l]][j]==1)
                   {
                    LocalBool = true;
                   }
                   
                   LogSoucin = LogSoucin || LocalBool; 
                }
                if (!LogSoucin)
                {
                    produkt = 0;
                    break;
                }
            }
            sum += produkt;
        }
        
        FixniIndex[FixniIndex.Length-CharakterCislo] = PocetModulu+1;// uvolneni indexu
        return sum;
    }
    else
    {
        
        int suma = 0;
        for (int i = 0;i<PocetModulu;i++) //suma nadrazenych sum cyklu
        {
             if (FixniIndex.Contains(i))
            {continue;}
            FixniIndex[FixniIndex.Length-CharakterCislo] = i;
            suma+=Rekurze(CharakterCislo-1,FixniIndex);
        }
        FixniIndex[FixniIndex.Length-CharakterCislo] = PocetModulu+1;// uvolneni indexove hodnoty pro nadrazene sum cykly
        return suma;
    }
}
double CharakteristickeCislo(int Verze)
{
    return  1/SpecialFunctions.Factorial(Verze)*Rekurze(Verze);
    
}
Console.WriteLine(CharakteristickeCislo(CharakterCisloVerze));
