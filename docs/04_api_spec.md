認証方式（JWT）への合意、ありがとうございます。これでフロントエンドとバックエンドをつなぐパイプラインが確立されました。

これまでの全ての要件（認証、論理削除、会場マスタ、PDF出力）を反映した\*\*「API仕様書」\*\*を作成しました。
これを `docs/04_api_spec.md` として保存してください。

Vue.js の開発者はこれを見ながら Axios の実装を行い、.NET の開発者はこれを見ながら Controller を実装することになります。

-----

### ファイル名: `docs/04_api_spec.md`

````markdown
# API仕様書 (API Specification)

## 1. 共通事項
* **Base URL**: `/api`
* **認証**:
    * ログイン以外の全てのエンドポイントで **Authorization Header** が必要。
    * 形式: `Authorization: Bearer <JWT_TOKEN>`
* **HTTPステータスコード**:
    * `200 OK`: 成功
    * `400 Bad Request`: バリデーションエラー
    * `401 Unauthorized`: 未ログインまたはトークン期限切れ
    * `404 Not Found`: データ無し
    * `500 Internal Server Error`: サーバー内部エラー

## 2. 認証 (Auth)

### 2.1 ログイン
* **URL**: `POST /auth/login`
* **概要**: チームIDとパスワードで認証し、JWTトークンを発行する。
* **Request Body**:
  ```json
  {
    "loginId": "myteam2025",
    "password": "password123"
  }
````

  * **Response (200 OK)**:
    ```json
    {
      "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...", // JWTトークン
      "expiresIn": 2592000, // 有効期限(秒) 例:30日
      "teamName": "FC Sonosono"
    }
    ```

## 3\. マスタ管理

### 3.1 選手一覧取得

  * **URL**: `GET /players`
  * **概要**: チームの全選手を取得する（論理削除済みを除く）。
  * **Response**:
    ```json
    [
      { "playerId": 1, "name": "田中", "uniformNumber": "10", "position": "MF" },
      { "playerId": 2, "name": "佐藤", "uniformNumber": "4", "position": "DF" }
    ]
    ```

### 3.2 選手追加

  * **URL**: `POST /players`
  * **Body**: `{ "name": "鈴木", "uniformNumber": "9", "position": "FW" }`

### 3.3 選手編集

  * **URL**: `PUT /players/{playerId}`
  * **Body**: `{ "name": "鈴木(弟)", "uniformNumber": "19", "position": "FW" }`

### 3.4 選手削除 (論理削除)

  * **URL**: `DELETE /players/{playerId}`
  * **概要**: `IsDeleted` フラグを true に更新する。

### 3.5 会場一覧取得

  * **URL**: `GET /venues`
  * **Response**:
    ```json
    [
      { "venueId": 1, "name": "市民グラウンド" },
      { "venueId": 2, "name": "県営サッカー場" }
    ]
    ```

### 3.6 会場追加

  * **URL**: `POST /venues`
  * **Body**: `{ "name": "河川敷Aコート" }`

### 3.7 会場削除 (論理削除)

  * **URL**: `DELETE /venues/{venueId}`

## 4\. 試合・フォーメーション管理

### 4.1 試合一覧取得

  * **URL**: `GET /matches`
  * **概要**: 試合のフォルダ一覧を取得する（日付の降順）。
  * **Response**:
    ```json
    [
      {
        "matchId": 101,
        "opponentName": "ライバルFC",
        "matchDate": "2024-12-01",
        "venueName": "市民グラウンド", // VenueIdまたはVenueTextから解決した値を返す
        "formationCount": 2 // 作成済みフォーメーション数
      }
    ]
    ```

### 4.2 試合詳細・フォーメーション取得

  * **URL**: `GET /matches/{matchId}`
  * **概要**: 試合の基本情報と、紐づく全てのフォーメーション詳細を取得する。
  * **Response**:
    ```json
    {
      "matchId": 101,
      "opponentName": "ライバルFC",
      "matchDate": "2024-12-01",
      "venueId": 1,
      "venueText": null,
      "formations": [
        {
          "formationId": 501,
          "label": "前半",
          "memo": "アグレッシブにプレスをかける",
          "pitchData": { ... } // JSONデータ (docs/03_database_schema.md 参照)
        },
        {
          "formationId": 502,
          "label": "後半",
          "memo": "15分: 鈴木IN",
          "pitchData": { ... }
        }
      ]
    }
    ```

### 4.3 試合作成 (フォルダ作成)

  * **URL**: `POST /matches`
  * **Body**:
    ```json
    {
      "opponentName": "ライバルFC",
      "matchDate": "2024-12-01",
      "venueId": 1,       // マスタから選んだ場合
      "venueText": null   // 手入力の場合
    }
    ```

### 4.4 フォーメーション保存 (一括更新)

  * **URL**: `POST /matches/{matchId}/formations`
  * **概要**: その試合に紐づくフォーメーションリストを丸ごと送信して保存する（追加・更新・削除を一括で行う）。
  * **Body**:
    ```json
    [
      {
        "formationId": 501, // 既存更新の場合はID指定、新規の場合はnullまたは0
        "label": "前半",
        "memo": "...",
        "pitchData": { ... }
      },
      {
        "formationId": 0,   // 新規追加
        "label": "後半",
        "memo": "...",
        "pitchData": { ... }
      }
    ]
    ```

### 4.5 試合PDFダウンロード

  * **URL**: `GET /matches/{matchId}/pdf`
  * **概要**: 指定した試合の全てのフォーメーションとメモをまとめたPDFを生成して返す。
  * **Response**: `Content-Type: application/pdf` (バイナリデータ)

<!-- end list -->

```

***

### 補足：PDFダウンロードの実装イメージ

最後の `GET /matches/{matchId}/pdf` がこのシステムの目玉機能です。



1.  **Vue.js**: 「PDFダウンロード」ボタンを押すと、`window.open('/api/matches/101/pdf')` する（または Blob として受け取ってダウンロード発火）。
2.  **.NET (Backend)**:
    * DBから試合情報、フォーメーション、選手リストを取得。
    * **Razor View** (.cshtml) にデータを流し込み、HTML文字列を生成（Vueの画面と似たデザインのHTMLを用意しておく）。
    * **PuppeteerSharp** を起動し、そのHTMLを読み込ませる。
    * `await page.PdfAsync()` でPDFバイナリ化。
    * ブラウザにファイルとして返却。

これでAPI仕様書も完成しました。

残るドキュメントはあと1つ、**「5. インフラ構成書 (`docs/05_infrastructure.md`)」** です。
AWSからGCP (Cloud Run) に変更になった部分を正確に記述し、CI/CDの流れも定義します。

**最後のドキュメント作成に進んでよろしいでしょうか？**
```