namespace LoteriaMexicanaTypes.Models;

public static class Cards
{
    public record Card(int Id, string Name);

    public static IReadOnlyList<Card> All { get; } =
    [
        new(1, "El Gallo"),
        new(2, "El Diablito"),
        new(3, "La Dama"),
        new(4, "El Catrín"),
        new(5, "El Paraguas"),
        new(6, "La Sirena"),
        new(7, "La Escalera"),
        new(8, "La Botella"),
        new(9, "El Barril"),
        new(10, "El Árbol"),
        new(11, "El Melón"),
        new(12, "El Valiente"),
        new(13, "El Gorrito"),
        new(14, "La Muerte"),
        new(15, "La Pera"),
        new(16, "La Bandera"),
        new(17, "El Bandolón"),
        new(18, "El Violoncello"),
        new(19, "La Garza"),
        new(20, "El Pájaro"),
        new(21, "La Mano"),
        new(22, "La Bota"),
        new(23, "La Luna"),
        new(24, "El Cotorro"),
        new(25, "El Borracho"),
        new(26, "El Negrito"),
        new(27, "El Corazón"),
        new(28, "La Sandía"),
        new(29, "El Tambor"),
        new(30, "El Camarón"),
        new(31, "Las Jaras"),
        new(32, "El Músico"),
        new(33, "La Araña"),
        new(34, "El Soldado"),
        new(35, "La Estrella"),
        new(36, "El Cazo"),
        new(37, "El Mundo"),
        new(38, "El Apache"),
        new(39, "El Nopal"),
        new(40, "El Alacrán"),
        new(41, "La Rosa"),
        new(42, "La Calavera"),
        new(43, "La Campana"),
        new(44, "El Cantarito"),
        new(45, "El Venado"),
        new(46, "El Sol"),
        new(47, "La Corona"),
        new(48, "La Chalupa"),
        new(49, "El Pino"),
        new(50, "El Pescado"),
        new(51, "La Palma"),
        new(52, "La Maceta"),
        new(53, "El Arpa"),
        new(54, "La Rana")
    ];
}