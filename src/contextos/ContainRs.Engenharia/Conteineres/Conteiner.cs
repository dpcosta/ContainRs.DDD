namespace ContainRs.Engenharia.Conteineres;

public record StatusConteiner(string Status)
{
    public static StatusConteiner ON => new("ON");
    public static StatusConteiner OFF => new("OFF");
    public static StatusConteiner STANDBY => new("STANDBY");
    public static StatusConteiner LOW_POWER => new("LOW_POWER");
    public static StatusConteiner FAULT => new("FAULT");
    public static StatusConteiner CHARGING => new("CHARGING");
    public override string ToString() => Status;
}

public class Conteiner
{
    public Guid Id { get; set; }
    /*
        ON - O contêiner está ligado e funcionando normalmente.
        OFF - O contêiner está desligado.
        STANDBY - O contêiner está em modo de espera, com consumo reduzido de energia.
        LOW_POWER - O contêiner está operando em um modo de baixa energia para economizar recursos.
        FAULT - Há uma falha no sistema de energia do contêiner.
        CHARGING - O contêiner está conectado a uma fonte de energia e sendo carregado. 
     */
    public StatusConteiner Status { get; set; } = StatusConteiner.OFF;
    public string? Observacoes { get; set; }
}
