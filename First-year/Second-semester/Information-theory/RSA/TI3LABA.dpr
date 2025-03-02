program TI3LABA;

uses
  Forms,
  UTI3LABA in 'UTI3LABA.pas' {FMain};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TFMain, FMain);
  Application.Run;
end.
