# データベース設計書 (Database Schema)

## 1. ER図 (Entity Relationship Diagram)

```mermaid
erDiagram
    Teams ||--o{ Players : "所属"
    Teams ||--o{ Venues : "所有"
    Teams ||--o{ Matches : "開催"
    Matches ||--o{ Formations : "構成"
    Venues |o--o{ Matches : "使用"

    Teams {
        bigint TeamId PK "自動採番"
        string LoginId UK "ログインID"
        string PasswordHash "ハッシュ化PW"
        string TeamName "チーム名"
        datetime CreatedAt "作成日時"
    }

    Players {
        bigint PlayerId PK "自動採番"
        bigint TeamId FK "チームID"
        string Name "選手名"
        string UniformNumber "背番号"
        string Position "基本ポジション"
        boolean IsDeleted "論理削除フラグ"
    }

    Venues {
        bigint VenueId PK "自動採番"
        bigint TeamId FK "チームID"
        string Name "会場名"
        boolean IsDeleted "論理削除フラグ"
    }

    Matches {
        bigint MatchId PK "自動採番"
        bigint TeamId FK "チームID"
        bigint VenueId FK "会場ID (任意)"
        string VenueText "手入力会場名 (任意)"
        string OpponentName "対戦相手"
        date MatchDate "試合日"
        datetime CreatedAt "作成日時"
    }

    Formations {
        bigint FormationId PK "自動採番"
        bigint MatchId FK "試合ID"
        string Label "区分"
        text Memo "交代・戦術メモ"
        jsonb PitchData "配置座標データ"
        datetime UpdatedAt "更新日時"
    }