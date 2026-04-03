using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modication
{
    public class SalaryCalculator
    {
        public double CalculateBaseSalary(double hours, double rate) //функция которая принимает ставку и кол-во часов
        {
            return hours * rate; //возвращаем произведение часов на ставку
        }
        //метод для расчета зп с учетом налога 13%
        public double CalculateNetSalary(double hours, double rate)
        {
            double gross = CalculateBaseSalary(hours, rate);
            double tax = gross * 0.13;
            return gross - tax;
        }
    }
    public class ModifiedSalaryCalculator : SalaryCalculator 
    {
        public new double CalculateNetSalary(double hours, double rate, double bonus = 0)//добавляется еще один параметр премия
        {
            double gross = CalculateBaseSalary(hours, rate); //базовая часть
            gross += bonus; //добавляем премию зп до вычета 
            //добавляем систему поэтапного налога
            double tax = 0;
            if (gross <= 25000)
                tax = gross * 0.10;//низкий налог для маленькой ЗП
            else
                tax = 25000 * 0.10 + (gross - 25000) * 0.20;//10% налога с первых 25000 + 20% с остатка
            return gross - tax;//возвращаем обновленную,чистую ЗП
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //тестирование исходного модуля
            SalaryCalculator oldCalc = new SalaryCalculator();
            double oldNet = oldCalc.CalculateNetSalary(160, 250);//создаем экземпляр класса до миодфикации
            //считаем зп до модификации 160 часов * 250 рублей
            Console.WriteLine($"Старая версия: {oldNet}");//отображение результата в консоль
            ModifiedSalaryCalculator newCalc = new ModifiedSalaryCalculator();//создаем экземпляр модифицированного класса
            double newNet = newCalc.CalculateNetSalary(160, 250, 3000);//считаем зп с учетом премии
            Console.WriteLine($"Новая версия: {newNet}");//отображаем консоль результаты модифицированного класса
            //демонстрация обратной совместимости (без премии)
            double noBonus = newCalc.CalculateNetSalary(160, 250);
            Console.WriteLine($"Без премии: {noBonus}");
        }
    }
}
