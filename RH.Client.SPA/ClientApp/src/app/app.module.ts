import { NgModule } from '@angular/core'
import { BrowserModule } from '@angular/platform-browser'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module'
import { AppComponent } from './app.component'
import { GraphQLModule } from './graphql.module'
import { LayoutComponent } from './components/layout/layout.component'
import { AuthGuard } from './guards/auth.guard'
import { AuthInterceptor } from './services/auth.interceptor'

@NgModule({
    declarations: [
        AppComponent,

        LayoutComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,

        AppRoutingModule,

        GraphQLModule,
        HttpClientModule,
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true
        }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
