import 'codemirror/lib/codemirror';
import 'codemirror/mode/sql/sql';
import 'codemirror/addon/edit/matchbrackets';
import 'codemirror/addon/hint/show-hint';
import 'codemirror/addon/hint/sql-hint';

import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { environment } from '@env/environment';

import { AppModule } from './app/app.module';

if (environment.production) {
    enableProdMode();
}

platformBrowserDynamic()
    .bootstrapModule(AppModule)
    .catch((err) => console.error(err));
