using System.Text.Json;
using Mapster;
using tlou_infected_api.Domain.DTO.Survivor;
using tlou_infected_api.Domain.Entities;
using tlou_infected_api.Domain.Enums;
using tlou_infected_api.Repository;

namespace tlou_infected_api.Application.Services;

public class SurvivorService(
    IMongoRepository<Survivor> survivorRepository,
    IKafkaProducerService kafkaProducerService)
{
    public async Task<Survivor> Create(SurvivorDto createSurvivorDto)
    {
        var survivor = createSurvivorDto.Adapt<Survivor>();
        await survivorRepository.AddAsync(survivor);
        return survivor;
    }

    public async Task<IEnumerable<Survivor>> GetAll()
    {
        return await survivorRepository.GetAllAsync();
    }

    public async Task<Survivor> GetSurvivorById(string id)
    {
        var survivor = await survivorRepository.GetByIdAsync(id);
        return survivor;
    }

    public async Task<string> GetSurvivorStatus(string id)
    {
        var survivor = await survivorRepository.GetByIdAsync(id);
        return SurvivorStatusEnum.GetName(typeof(SurvivorStatusEnum), survivor.Status);
    }

    public async Task<bool> UpdateSurvivor(SurvivorDto createSurvivorDto)
    {
        var survivorUpdate = createSurvivorDto.Adapt<Survivor>();
        await survivorRepository.UpdateAsync(createSurvivorDto.Id, survivorUpdate);
        return true;
    }

    public async Task<bool> DeleteSurvivor(string id)
    {
        await survivorRepository.DeleteAsync(id);
        return true;
    }

    // Serializando o payload da request e delegando ao producer injetado
    public async Task SendMessageAsync(string topic, string key, object payload)
    {
        var json = JsonSerializer.Serialize(payload);
        await kafkaProducerService.SendMessageAsync(topic, json);
    }
}