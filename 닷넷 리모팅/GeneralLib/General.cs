using System;

namespace GeneralLib
{
    public class General : MarshalByRefObject
    {
        public string ConvertIntToStr(int num)
        {
            Console.WriteLine($"ConvertIntToStr 메소드 수행(전달 받은 인자:{num}");
            switch(num)
            {
                case 0: return "영";
                case 1: return "일";
                case 2: return "이";
                default: return "아직 모르는 수 입니다.";
            }
        }
    }
}
