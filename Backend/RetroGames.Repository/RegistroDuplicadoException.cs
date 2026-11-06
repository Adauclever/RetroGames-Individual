namespace RetroGames.Repository;
public class RegistroDuplicadoException(Exception causa)
    : Exception("Já existe um registro com o valor informado.", causa);
