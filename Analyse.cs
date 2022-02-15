namespace Components
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    internal class Analyse
    {
        public RegisterNumber CheckRegisterNumber(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (value.Length > 15)
                    return new RegisterNumber() { Valid = false };

                //Проверка ОГРНИП
                if (value.Length == 15 || value.Length == 13)
                {
                    RegisterNumber registerNumber = new RegisterNumber();

                    char[] digits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
                    string converted = String.Join("", value.Where(x => digits.Contains(x)).ToArray());

                    if (converted != null && converted.Length == 15)
                    {
                        if (converted[0] == '3')
                            registerNumber.Egrip = true;

                        if (int.TryParse(converted.Substring(1, 2), out int year))
                            registerNumber.Year = year;

                        if (int.TryParse(converted.Substring(3, 2), out int region))
                            registerNumber.Region = region;

                        if (int.TryParse(converted.Substring(5, 9), out int record))
                            registerNumber.Record = record;

                        if (int.TryParse(converted.Substring(14, 1), out int control))
                            registerNumber.Control = control;

                        if (decimal.TryParse(converted.Substring(0, 14), out decimal math))
                        {
                            decimal step1 = Math.Floor(math / 13);
                            decimal step2 = Math.Floor(step1 * 13);
                            decimal step3 = math - step2;

                            if (step3 == decimal.Parse(converted.Substring(14, 1)))
                            {
                                registerNumber.Valid = true;
                                registerNumber.Result = converted;

                                return registerNumber;
                            }
                        }
                    }
                    else if (converted != null && converted.Length == 13)
                    {
                        switch (converted[0])
                        {
                            case '1':
                                registerNumber.Egrul = true;
                                break;
                            case '5':
                                registerNumber.Egrul = true;
                                break;
                            case '2':
                                registerNumber.Egrul = true;
                                break;
                            case '4':
                                registerNumber.Egrul = true;
                                break;
                            case '6':
                                registerNumber.Egrul = true;
                                break;
                            case '7':
                                registerNumber.Egrul = true;
                                break;
                            case '8':
                                registerNumber.Egrul = true;
                                break;
                            case '9':
                                registerNumber.Egrul = true;
                                break;
                        }

                        if (int.TryParse(converted.Substring(1, 2), out int year))
                            registerNumber.Year = year;

                        if (int.TryParse(converted.Substring(3, 2), out int region))
                            registerNumber.Region = region;

                        if (int.TryParse(converted.Substring(5, 7), out int record))
                            registerNumber.Record = record;

                        if (int.TryParse(converted.Substring(12, 1), out int control))
                            registerNumber.Control = control;

                        if (decimal.TryParse(converted.Substring(0, 12), out decimal math))
                        {
                            decimal step1 = Math.Floor(math / 11);
                            decimal step2 = Math.Floor(step1 * 11);
                            decimal step3 = math - step2;

                            if ((int)step3 == registerNumber.Control)
                            {
                                registerNumber.Valid = true;
                                registerNumber.Result = converted;

                                return registerNumber;
                            }
                        }
                    }



                }
            }

            return new RegisterNumber();
        }
    }

    /// <summary>
    /// Описание регистрационного номера
    /// ОГРН (номер юрлица) состоит из 13 знаков вида: С Г Г К К Х Х Х Х Х Х Х Ч
    /// ОГРНИП (номер ИП) — из 15 знаков вида: С Г Г К К Х Х Х Х Х Х Х Х Х Ч
    /// </summary>
    public class RegisterNumber
    {
        /// <summary>
        /// Статус действительности данного регистрационного номера
        /// </summary>
        internal bool Valid { get; set; }

        /// <summary>
        /// Год регистрационного номера
        /// </summary>
        internal int Year { get; set; }

        /// <summary>
        /// Регион
        /// </summary>
        internal int Region { get; set; }

        /// <summary>
        /// Текущий реестр является ЕГРИП
        /// </summary>
        internal bool Egrip { get; set; }

        /// <summary>
        /// Текущий реестр является ЕГРЮЛ
        /// </summary>
        internal bool Egrul { get; set; }

        /// <summary>
        /// ХХХХХХХ (с 6-го по 12-й знак) — номер записи, внесённой в течение года в ЕГРЮЛ; ХХХХХХХХХ (с 6-го по 14-й знак) — в ЕГРИП;
        /// </summary>
        internal int Record { get; set; }

        /// <summary>
        /// Итоговое значение ОГРН/ОГРНИП или ИНН
        /// </summary>
        internal string Result { get; set; }

        /// <summary>
        /// Ч (последний) — контрольная цифра. Она равна младшему разряду остатка от деления числа, состоящего из первых 12 цифр, на 11 (для юрлиц) или 14-значного числа на 13 (для ИП). Если остаток больше 9, контрольная цифра равна последней цифре остатка.
        /// </summary>
        internal int Control { get; set; }
    }
}
