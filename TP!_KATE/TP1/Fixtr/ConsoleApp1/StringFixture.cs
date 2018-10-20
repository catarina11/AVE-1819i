using System;
using System.Reflection;

namespace ConsoleApp1
{
    internal class StringFixture : GeneratorIFixture
    {
        Random rnd = new Random();

        public StringFixture(Type t) : base(t)
        {

        }

        public override IFixture Member(string v)
        {
            throw new NotImplementedException();
        }

        public override object New()
        {
            return GenerateString(rnd.Next(0, 50));
        }
        private string GenerateString(int length)
        {
            //gera uma string aleatoria
            const string alfa = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
            char[] randomChar = new char[length];
            for (int i = 0; i < length; i++)
                randomChar[i] = alfa[rnd.Next(0, alfa.Length)];
            return new string(randomChar);


        }
    }
}