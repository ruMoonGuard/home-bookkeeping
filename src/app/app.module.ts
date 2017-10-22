import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { AuthModule } from './auth/auth.module';
import { AppRoutingModule } from './app-routing.module';
import { UsersService } from './shared/services/users.service';
import { AuthService } from './shared/services/auth.service';
import { AuthGuard } from './shared/services/auth.guard';
import { HttpNotFoundComponent } from './components/http-not-found/http-not-found.component';

@NgModule({
  declarations: [
    AppComponent,
    HttpNotFoundComponent
  ],
  imports: [
    BrowserModule,
    AuthModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpModule
  ],
  providers: [UsersService, AuthService, AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
