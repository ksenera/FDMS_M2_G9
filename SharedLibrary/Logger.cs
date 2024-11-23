/*
 * File          : .cs
 * Project       : SENG3020 M-02
 * Programmer(s) : Kushika Senera #8837130, Andrew Babos #8822549 & Rhys McCash #8825169
 * First Version : 11/21/2024
 * Description   : 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary
{
    public interface ILogger
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogDebug(string message);
    }
}
