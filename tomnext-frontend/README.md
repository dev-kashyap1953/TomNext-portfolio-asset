# TomNext Frontend (Angular)

✅ **Overview**

This is the Angular frontend for the TomNext Portfolio Assets app. The project uses **Angular 21.x** and **@angular/material** (Angular Material) is included in `package.json`.

---

## Requirements

- Node.js (recommended: Node 20+)
- npm (package manager in `package.json` is `npm@11.x` but `npm` shipped with Node 20+ is fine)
- Angular CLI (optional; you can use the local CLI via `npx` or `npm` scripts)

---

## Install Dependencies

From this project folder:

```bash
cd tomnext-frontend
npm install
```

---

## Run Local Development Server

Start dev server (runs `ng serve`):

```bash
npm start
# or
npm run start
```

The app will be available at `http://localhost:4200/` by default. The frontend expects the API base URL in `src/environments/environments.ts` (by default it points to `http://localhost:5090/api/`). Change that value to point to your backend as needed.

---

## Quick Recap

- Install: `npm install`
- Start: `npm start`

---

If you'd like, I can add a small dev script to start both the backend and frontend together (concurrently) for local development. 💡
