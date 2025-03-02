program TI2LABA;

uses
  Forms,
  UTI2LABA in 'UTI2LABA.pas' {FMain};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TFMain, FMain);
  Application.Run;
end.
