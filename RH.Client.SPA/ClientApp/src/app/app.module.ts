import { NgModule } from '@angular/core'
import { BrowserModule } from '@angular/platform-browser'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'

import { AppRoutingModule } from './app.routing'
import { RootComponent } from './root.component'
import { GraphQLModule } from './graphql.module'
import { DefaultLayoutComponent } from './layouts/default-layout/default-layout.component'
import { AuthInterceptor } from './interceptors/auth.interceptor'

@NgModule({
    declarations: [
        RootComponent,

        DefaultLayoutComponent
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
    bootstrap: [RootComponent]
})
export class AppModule { }
