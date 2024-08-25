using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMCalloutsRemastered.Stuff
{
    // Credit to Yobbin for his Open Source code as a reference. github.com/YobB1n/YobbinCallouts/blob/master/UltimateBackupHelper.cs

    internal class UltimateBackupFunctions
    {
        internal static void CallPursuitBackup()
        {
            try
            {
                UltimateBackup.API.Functions.callPursuitBackup();
            }
            catch (FileNotFoundException)
            {

            }
            catch (Exception)
            {

            }
        }

        internal static void callCode3BackUp()
        {
            try
            {
                UltimateBackup.API.Functions.callCode3Backup();
            }
            catch (FileNotFoundException)
            {

            }
            catch (Exception)
            {

            }
        }

        internal static void callCode3SwatBackup(bool radio = false, bool isNooseUnit = false)
        {
            try
            {
                UltimateBackup.API.Functions.callCode3SwatBackup(radio, isNooseUnit);
            }
            catch (FileNotFoundException)
            {

            }
            catch (Exception)
            {

            }
        }

        internal static void callFireDepartment()
        {
            try
            {
                UltimateBackup.API.Functions.callFireDepartment();
            }
            catch (FileNotFoundException)
            {

            }
            catch (Exception)
            {

            }
        }

        internal static void callTrafficStopBackup(bool radio = false, bool isLocalPatrol = false)
        {
            try
            {
                UltimateBackup.API.Functions.callTrafficStopBackup(radio, isLocalPatrol);
            }
            catch (FileNotFoundException)
            {

            }
            catch (Exception)
            {

            }
        }
    }
}
