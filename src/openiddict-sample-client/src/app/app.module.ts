import { NgModule            } from '@angular/core';
import { HttpClientModule    } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule       } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent     } from './app.component';
import { SigninComponent  } from './components';
import { HomeComponent    } from './components';
import { AuthorizationModule } from './authorization';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    SigninComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule,

    AuthorizationModule.forRoot(
      "https://localhost:5004/",
      "http://localhost:5005",
      "openiddict-sample-api",
      "openiddict-sample-api-scope"),

    AppRoutingModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
