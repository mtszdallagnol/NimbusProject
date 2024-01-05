using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Nimbus.Api.Dtos.NimbusBox;
using Nimbus.Api.Dtos.UserNimbus;

namespace Nimbus.Api.Services
{
    public class NimbusBoxService : IBaseCrudService
    {
        private readonly DataContext _context;
        private readonly IHubContext<StateResultHub> _hubContext;

        public NimbusBoxService(DataContext context, IHubContext<StateResultHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<object> addRegister<T>(T newSave)
        {
            try
            {
                NimbusBox box = newSave as NimbusBox
                    ?? throw new Exception("Os dados não podem ser nulos!");
            
                box.dtCriacao = DateTime.Now;

                _context.tbNimbusData.Add(box);

                await _context.SaveChangesAsync();

                NimbusOnlyData wssSend = new()
                {
                    nrTemperatura = (double)box.nrTemperatura!,
                    nrPressao = (double)box.nrPressao!,
                    nrUmidade = (double)box.nrUmidade!,
                    nrEspectroLuz = (double)box.nrEspectroLuz!,
                    nrInfraVermelho = (double)box.nrInfraVermelho!,
                    nrLuzUv = (double)box.nrLuzUv!,
                    nrAguaAltura = (double)box.nrAguaAltura!,
                    nrVelVento = (double)box.nrVelVento!
                };

                var temp = await _context.tbNimbusData.ToListAsync();
                List<DefaultNimbusBox> finalTemp = temp.ConvertAll(x => new DefaultNimbusBox
                {
                    idNimbus = x.idNimbus,
                    idUserNimbus = x.idUserNimbus,
                    nrTemperatura = x.nrTemperatura,
                    nrUmidade = x.nrUmidade,
                    nrPressao = x.nrPressao,
                    nrEspectroLuz = x.nrEspectroLuz,
                    nrInfraVermelho = x.nrInfraVermelho,
                    nrLuzUv = x.nrLuzUv,
                    nrAguaAltura = x.nrAguaAltura,
                    dtCriacao = x.dtCriacao,
                });

                var grupos = finalTemp.GroupBy(x => x.idUserNimbus).Select(grupo => grupo.OrderByDescending(x => x.dtCriacao).Take(5));

                NimbusOnlyDataWithRecentWithId dataToSend = new()
                {
                    listMedium5days = grupos.Cast<object>().ToList(),
                    nimbusMostRecent = wssSend,
                    currentIdNimbusProduct = box.idUserNimbus
                };

                await _hubContext.Clients.All.SendAsync("RecievedMessage", dataToSend);

                return await _context.tbNimbusData.ToListAsync();
            } catch
            {
                throw new Exception("Algo interno no sistema deu errado.");
            }
        }

        public async Task<object> deleteRegister<T>(T _id)
        {
            var RmvBox = await _context.tbNimbusData.SingleOrDefaultAsync(x => x.idNimbus == Convert.ToInt32(_id)) 
                ?? throw new Exception("O produto não existe!");

            _context.tbNimbusData.Remove(RmvBox);
            await _context.SaveChangesAsync();
            return await _context.tbNimbusData.ToListAsync();
        }

        public async Task<object> getAllRegisters()
        {
            return await _context.tbNimbusData.ToListAsync();
        }

        public async Task<object> getById<T>(T _id)
        { 
            return await _context.tbNimbusData.SingleOrDefaultAsync(x => x.idNimbus == Convert.ToInt32(_id)) 
                ?? throw new Exception("O produto não existe");
        }

        public async Task<object> getAllNimbusDataMedium()
        {
            List<NimbusBox> allProducts = await _context.tbNimbusData.ToListAsync();
            List<NimbusOnlyData> response = new();

            response = setAllMediumValuesInMonth(allProducts);

            return response;
        }

        private List<NimbusOnlyData> setAllMediumValuesInMonth(List<NimbusBox> allProducts)
        {
            List<NimbusOnlyData> response = new();
            var groupedByMonthNimbus = allProducts.GroupBy(x => new { x.dtCriacao });

            foreach (var group in groupedByMonthNimbus)
            {
                double tempNrTemperatura = 0, tempNrPressao = 0, tempNrUmidade = 0, tempNrEspectroLuz = 0, tempNrInfraVermelho = 0, tempNrLuzUv = 0, tempNrAguaAltura = 0, tempNrVelVento = 0;
                int countCurrentGroup = group.Count();
                foreach (var ms in group)
                {
                    tempNrTemperatura = tempNrTemperatura + (double)ms.nrTemperatura!;
                    tempNrPressao = tempNrPressao + (double)ms.nrPressao!;
                    tempNrUmidade = tempNrUmidade + (double)ms.nrUmidade!;
                    tempNrEspectroLuz = tempNrEspectroLuz + (double)ms.nrEspectroLuz!;
                    tempNrInfraVermelho = tempNrInfraVermelho + (double)ms.nrInfraVermelho!;
                    tempNrLuzUv = tempNrLuzUv + (double)ms.nrLuzUv!;
                    tempNrAguaAltura = tempNrAguaAltura + (double)ms.nrAguaAltura!;
                    tempNrVelVento = tempNrVelVento + (double)ms.nrVelVento!;
                }

                response.Add(new NimbusOnlyData
                {
                    nrTemperatura = ((double)((int)((tempNrTemperatura / countCurrentGroup) * 1000))) / 1000.0,
                    nrPressao = ((double)((int)((tempNrPressao / countCurrentGroup) * 1000))) / 1000.0,
                    nrUmidade = ((double)((int)((tempNrUmidade / countCurrentGroup) * 1000))) / 1000.0,
                    nrEspectroLuz = ((double)((int)((tempNrEspectroLuz / countCurrentGroup) * 1000))) / 1000.0,
                    nrInfraVermelho = ((double)((int)((tempNrInfraVermelho / countCurrentGroup) * 1000))) / 1000.0,
                    nrLuzUv = ((double)((int)((tempNrLuzUv / countCurrentGroup) * 1000))) / 1000.0,
                    nrAguaAltura = ((double)((int)((tempNrAguaAltura / countCurrentGroup) * 1000))) / 1000.0,
                    nrVelVento = ((double)((int)((tempNrVelVento / countCurrentGroup) * 1000))) / 1000.0
                });
            }

            return response;
        }

        public async Task<object> getAllNimbusDataMediumById(int nimbusUserId)
        {
            List<NimbusBox> allProductsWithId = await _context.tbNimbusData.Where(x => x.idUserNimbus == nimbusUserId).ToListAsync();
            var temp = allProductsWithId.OrderByDescending(x => x.dtCriacao);
            NimbusOnlyData tempRecentRegister = new()
            {
                nrTemperatura = (double)temp.FirstOrDefault()!.nrTemperatura!,
                nrPressao = (double)temp.FirstOrDefault()!.nrPressao!,
                nrUmidade = (double)temp.FirstOrDefault()!.nrUmidade!,
                nrEspectroLuz = (double)temp.FirstOrDefault()!.nrEspectroLuz!,
                nrInfraVermelho = (double)temp.FirstOrDefault()!.nrInfraVermelho!,
                nrLuzUv = (double)temp.FirstOrDefault()!.nrLuzUv!,
                nrAguaAltura = (double)temp.FirstOrDefault()!.nrAguaAltura!,
                nrVelVento = (double)temp.FirstOrDefault()!.nrVelVento!
            };
            NimbusOnlyDataWithRecent response = new();
            IEnumerable<NimbusBox> tempIntermediery = temp.Take(5);

            response = new()
            {
                listMediumMonts = tempIntermediery.Cast<object>().ToList(),
                nimbusMostRecent = tempRecentRegister
            };

            return response;
        }

    }
}