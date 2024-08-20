using System.Collections.Generic;
using System.Linq;
using System.IO;
using Rage;
using System;

namespace JMCalloutsRemastered.Stuff
{
    internal class StopThePedFunctions
    {
        internal static void SetPedUnderTheInfluence(Ped ped, bool underInfluence)
        {
            try
            {
                StopThePed.API.Functions.setPedAlcoholOverLimit(ped, underInfluence);
            }
            catch (FileNotFoundException)
            {

            }
            catch (Exception)
            {

            }
        }
        internal static void SetPedUnderDrugInfluence(Ped ped, bool overDrugInfluence)
        {
            try
            {
                StopThePed.API.Functions.setPedUnderDrugsInfluence(ped, overDrugInfluence);
            }
            catch (FileNotFoundException)
            {

            }
            catch (Exception)
            {

            }
        }
        internal static void injectPedItems(Ped ped)
        {
            try
            {
                StopThePed.API.Functions.injectPedSearchItems(ped);
            }
            catch (FileNotFoundException)
            {

            }
            catch (Exception)
            {

            }
        }
        internal static void InjectVehicleItems(Vehicle vehicle)
        {
            try
            {
                StopThePed.API.Functions.injectVehicleSearchItems(vehicle);
            }
            catch (FileNotFoundException)
            {

            }
            catch (Exception)
            {

            }
        }
        internal static void CallTowService()
        {
            try
            {
                StopThePed.API.Functions.callTowService();
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
