﻿using System;
using System.Reflection;

namespace Exercicios
{
    class Student
    {
        [Validator("NumberGreaterThan20000")]
        public int Nr { get; private set; }

        public string Name { get; private set; }

        public Student(int number, string name) {
            Nr = number;
            Name = name;
        }
    }

    class NumberValidator {
        // ...
        public static bool IsValid(Type studentType, int value) {
            // Verifica se propriedade Nr tem atributo Validator aplicado. 
            // Se sim, verifica se value é um argumento válido, executando o método
            // NumberGreaterThan20000.
            // O método NumberGreaterThan20000 encontra-se na classe ValidatorAttribute
            // Otimize o código para criar o atributo apenas uma vez.

            /*obtemos todas as propriedades de Student: Nr, Name*/
            var propertiesOfStudent = studentType.GetProperties(); 

            /*verifica se a propriedade Nr tem o atibuto Validator*/
            var hasAttr = Attribute.IsDefined(studentType.GetProperty("Nr"), typeof(ValidatorAttribute));

            
            if (hasAttr)
              return ValidatorAttribute.NumberGreaterThan20000(value);

            else
                return true;
        }
       
    }


    class App {
        public static void Main() {
            bool validArg = NumberValidator.IsValid(typeof(Student), 19000); // false
            Console.WriteLine(validArg);

            validArg = NumberValidator.IsValid(typeof(Student), 25000); // true
            Console.WriteLine(validArg);
        }
    }
}
