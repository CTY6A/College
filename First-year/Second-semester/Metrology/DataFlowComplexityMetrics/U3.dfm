object FMain: TFMain
  Left = 350
  Top = 123
  Width = 1018
  Height = 426
  Caption = '-'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'Tahoma'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object L1: TLabel
    Left = 259
    Top = 165
    Width = 593
    Height = 13
    Caption = 
      #1055#1086#1083#1085#1072#1103' '#1084#1077#1090#1088#1080#1082#1072' '#1063#1077#1087#1080#1085#1072'                                           ' +
      '             |                                              '#1052#1077#1090#1088 +
      #1080#1082#1072' '#1063#1077#1087#1080#1085#1072' '#1074#1074#1086#1076#1072'/'#1074#1099#1074#1086#1076#1072
    Visible = False
  end
  object L2: TLabel
    Left = 467
    Top = 72
    Width = 83
    Height = 13
    Caption = #1057#1087#1077#1085' '#1087#1088#1086#1075#1088#1072#1084#1084#1099
    Visible = False
  end
  object BMain: TButton
    Left = 357
    Top = 8
    Width = 297
    Height = 49
    Caption = #1054#1090#1082#1088#1099#1090#1100' '#1092#1072#1081#1083' '#1089' '#1080#1089#1093#1086#1076#1085#1099#1084' '#1082#1086#1076#1086#1084
    TabOrder = 0
    OnClick = BMainClick
  end
  object SGChep: TStringGrid
    Left = 0
    Top = 184
    Width = 1009
    Height = 185
    ColCount = 9
    DefaultColWidth = 109
    RowCount = 2
    Options = [goFixedVertLine, goFixedHorzLine, goVertLine, goHorzLine, goRangeSelect, goThumbTracking]
    TabOrder = 1
    Visible = False
    RowHeights = (
      24
      24)
  end
  object SGSpen: TStringGrid
    Left = 0
    Top = 88
    Width = 1009
    Height = 73
    ColCount = 2
    DefaultColWidth = 142
    RowCount = 2
    Options = [goFixedVertLine, goFixedHorzLine, goVertLine, goHorzLine, goRangeSelect, goThumbTracking]
    TabOrder = 2
    Visible = False
    RowHeights = (
      24
      24)
  end
  object SG1: TStringGrid
    Left = 0
    Top = 368
    Width = 1009
    Height = 25
    ColCount = 3
    DefaultColWidth = 334
    RowCount = 1
    FixedRows = 0
    TabOrder = 3
    Visible = False
  end
  object DOMain: TOpenDialog
    Filter = #1048#1089#1093#1086#1076#1085#1099#1081' '#1082#1086#1076'|*.txt'
    Left = 56
    Top = 8
  end
end
