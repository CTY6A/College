program Project1;

uses
  Forms,
  Unit1 in 'Unit1.pas' {FShifr};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TFShifr, FShifr);
  Application.Run;
end.
