u s i n g   U n i t y E n g i n e ; 
 u s i n g   S y s t e m . C o l l e c t i o n s ; 
 
 p u b l i c   c l a s s   C a n o n Q u e s t   :   Q u e s t B a s e     { 
 
 	 p r i v a t e   c o n s t   i n t   T O T A L _ C A N O N B A L L S   =   5 ; 
 	 p r i v a t e   i n t   c a n o n b a l l s _ s h o t ; 
 
 	 p r i v a t e   c o n s t   f l o a t   R O T A T I O N _ S P E E D   =   1 0 ; 
 	 p r i v a t e   V e c t o r 3   s t a r t _ s i d e _ r o t a t i o n ; 
 	 p r i v a t e   V e c t o r 3   s t a r t _ u p _ r o t a t i o n ; 
 
 	 p r i v a t e   c o n s t   f l o a t   R E L O A D _ T I M E   =   5 f ; 
 	 p r i v a t e   f l o a t   r e l o a d _ t i m e r ; 
 
 
 	 p r i v a t e   c o n s t   f l o a t   E N D _ T I M E R _ F A I L   =   5 f ; 
 	 p r i v a t e   c o n s t   f l o a t   E N D _ T I M E R _ C O M P L E T E   =   1 5 f ; 
 	 p r i v a t e   f l o a t   e n d _ t i m e r ; 
 
 	 p r i v a t e 	 c o n s t   f l o a t   M I S S E D _ S H I P _ T I M E R   =   5 5 f ; 
 	 p r i v a t e   f l o a t   s h i p _ t i m e r ; 
 
 	 p r i v a t e   b o o l   c a n o n b a l l _ i n _ a i r   =   f a l s e ; 
 
 	 p r i v a t e   i n t   n r _ o f _ h i t s   =   0 ; 
 
 	 p r i v a t e   P a r t i c l e S y s t e m   s m o k e ; 
 
 	 p r i v a t e   V e c t o r 3   p r e v P o s ; 
 
 	 G a m e O b j e c t   m a i n C a m e r a ; 
 	 G a m e O b j e c t   c a n o n C a m e r a ; 
 	 G a m e O b j e c t   c a n o n ; 
 	 G a m e O b j e c t   c a n o n P i p e ; 
 	 G a m e O b j e c t   s h i p ; 
 	 G a m e O b j e c t   p l a y e r ; 
 	 G a m e O b j e c t   c a n o n B a l l ; 
 	 G a m e O b j e c t   c a n o n M u z z l e ; 
 	 G a m e O b j e c t   c a n o n B a s e ; 
 	 G a m e O b j e c t   r e m i n d e r ; 
 	 G a m e O b j e c t   e n d D i a g ; 
 
 	 p u b l i c   T e x t u r e   c a n o n b a l l _ t e x t u r e ; 
 	 p u b l i c   T e x t u r e   r e l o a d b a r _ t e x t u r e ; 
 
 	 p u b l i c   T e x t u r e   a r r o w _ u p _ t e x t u r e ; 
 	 p u b l i c   T e x t u r e   a r r o w _ d o w n _ t e x t u r e ; 
 	 p u b l i c   T e x t u r e   a r r o w _ r i g h t _ t e x t u r e ; 
 	 p u b l i c   T e x t u r e   a r r o w _ l e f t _ t e x t u r e ; 
 	 p u b l i c   T e x t u r e   f i r e _ t e x t u r e ; 
 
 	 p r i v a t e   b o o l   q u e s t A c t i v e   =   f a l s e ; 
 
 	 G U I T e x t u r e   l e f t _ a r r o w ; 
 	 G U I T e x t u r e   r i g h t _ a r r o w ; 
 	 G U I T e x t u r e   u p _ a r r o w ; 
 	 G U I T e x t u r e   d o w n _ a r r o w ; 
 	 G U I T e x t u r e   s h o o t _ g u i ; 
 	 
 	 G U I T e x t u r e [ ]   g u i L i s t   =   n e w   G U I T e x t u r e [ 5 ] ; 
 
 	 / / C a l l e d   w h e n   p l a y e r   s t a r t s   q u e s t 
 	 p u b l i c   o v e r r i d e   v o i d   T r i g g e r S t a r t   ( ) 
 	 { 
 	 	 m a i n C a m e r a   	 =   G a m e O b j e c t . F i n d   ( " M a i n   C a m e r a " ) ; 
 	 	 p l a y e r 	 	 =   G a m e O b j e c t . F i n d G a m e O b j e c t W i t h T a g   ( " P l a y e r " ) ; 
 	 	 c a n o n   	 	 =   G a m e O b j e c t . F i n d G a m e O b j e c t W i t h T a g   ( " K a n o n " ) ; 
 	 	 c a n o n P i p e   	 =   G a m e O b j e c t . F i n d G a m e O b j e c t W i t h T a g   ( " K a n o n P i p a " ) ; 
 	 	 c a n o n M u z z l e   =   G a m e O b j e c t . F i n d   ( " K a n o n M y n n i n g " ) ; 
 	 	 c a n o n B a s e 	 =   G a m e O b j e c t . F i n d   ( " K a n o n B a s " ) ; 
 	 	 s m o k e   	 	 =   G a m e O b j e c t . F i n d   ( " C a n o n S m o k e " ) . G e t C o m p o n e n t   ( " P a r t i c l e S y s t e m " )   a s   P a r t i c l e S y s t e m ; 
 
                 ( ( G U I T e x t u r e ) ( G a m e O b j e c t . F i n d ( " K a r t a " ) ) . G e t C o m p o n e n t I n C h i l d r e n ( t y p e o f ( G U I T e x t u r e ) ) ) . e n a b l e d   =   f a l s e ; 
 
 	 	 s h i p   =   ( G a m e O b j e c t ) I n s t a n t i a t e ( R e s o u r c e s . L o a d   ( " S h i p " ) ) ; 
 
 
 	 	 p r e v P o s   =   ( p l a y e r . t r a n s f o r m . p o s i t i o n ) ; 
 / / 	 	 G a m e O b j e c t   t e m p   =   G a m e O b j e c t . F i n d   ( " G r a p h i c s " ) ; 
 / / 	 	 t e m p . r e n d e r e r . e n a b l e d   =   f a l s e ; 
 
 / / 	 	 E v e n t M a n a g e r . T r i g g e r D i s a b l e A n d r o i d ( " l o c k " ) ; 
 	 	 m a i n C a m e r a . c a m e r a . e n a b l e d   =   f a l s e ; 
 	 	 c a n o n C a m e r a . c a m e r a . e n a b l e d   =   t r u e ; 
 
 	 	 c a n o n b a l l s _ s h o t   =   0 ; 
 
 	 	 q u e s t A c t i v e   =   t r u e ; 
 	 	 i f   ( s m o k e )   { 
 	 	 	 s m o k e . C l e a r   ( ) ; 
 	 	 	 s m o k e . S t o p   ( ) ; 
 	 	 } 
 
 	 	 r e m i n d e r . S e t A c t i v e   ( t r u e ) ; 
 	 	 ( ( R e m i n d e r T e x t S c r i p t ) r e m i n d e r . G e t C o m p o n e n t < R e m i n d e r T e x t S c r i p t > ( ) ) . C h a n g e T e x t ( " S i k t a   k a n o n e n   m o t   b � t e n   m e d   p i l a r n a .   D u   h a r   5   s k o t t   p �   d i g   o c h   e n   b e g r � n s a d   t i d   f � r   a t t   s � n k a   s k e p p e t . " ) ; 
 
 	 	 I n i t   ( ) ; 
 	 } 
 
 	 / / C a l l e d   w h e n   p l a y e r   f i n i s h e s   q u e s t 
 	 p u b l i c   o v e r r i d e   v o i d   T r i g g e r F i n i s h   ( b o o l   s u c c e s s ) 
 	 { 
 	 	 b a s e . T r i g g e r F i n i s h   ( s u c c e s s ) ; 
 	 	 / / R e m o v e s   a r r o w s   o n   s c r e e n 
 	 	 G a m e O b j e c t   t   =   G a m e O b j e c t . F i n d   ( " C a n o n G U I " ) ; 
 	 	 D e s t r o y   ( t ) ; 
 	 	 D e s t r o y   ( s h i p ) ; 
 	 	 q u e s t A c t i v e   =   f a l s e ; 
 / / 	 	 E v e n t M a n a g e r . T r i g g e r D i s a b l e A n d r o i d ( " u n l o c k " ) ; 
 	 	 m a i n C a m e r a . c a m e r a . e n a b l e d   =   t r u e ; 
 	 	 c a n o n C a m e r a . c a m e r a . e n a b l e d   =   f a l s e ; 
 
 	 
 	 	 i f   ( n r _ o f _ h i t s   >   0 )   { 
 	 	 	 e n d D i a g . G e t C o m p o n e n t < e n d N o t i f i c a t i o n S c r i p t >   ( ) . A c t i v a t e ( " H � r l i g t ,   d u   s � n k t e   s k e p p e t . " ) ; 
 	 	 	 i f   ( P l a y e r P r e f s . G e t I n t   ( " S q u e s t " )   = =   0 ) 
 	 	 	 	 P l a y e r P r e f s . S e t I n t   ( " S q u e s t " ,   2 ) ; 
 	 	 	 e l s e   i f   ( P l a y e r P r e f s . G e t I n t   ( " S q u e s t " )   = =   1 ) 
 	 	 	 	 P l a y e r P r e f s . S e t I n t   ( " S q u e s t " ,   3 ) ; 
 	 	 	 	 } 
 	 	 	 	 e l s e   i f   ( n r _ o f _ h i t s   = =   0   & &   c a n o n b a l l s _ s h o t   = =   5 ) 
 	 	 	 	 	 e n d D i a g . G e t C o m p o n e n t < e n d N o t i f i c a t i o n S c r i p t >   ( ) . A c t i v a t e ( " T u s a n ,   s k e p p e t   k o m   f � r b i ! " ) ; 
 	 	 	 	 e l s e   i f   ( n r _ o f _ h i t s   = =   0   & &   c a n o n b a l l s _ s h o t   <   5 ) 
 	 	 	 	 	 e n d D i a g . G e t C o m p o n e n t < e n d N o t i f i c a t i o n S c r i p t >   ( ) . A c t i v a t e ( " H e l v e t e ,   d u   m i s s a d e   a l l a   k a n o n k u l o r . " ) ; 
 	 	 e n d D i a g . S e t A c t i v e   ( t r u e ) ; 
 	 	 p l a y e r . t r a n s f o r m . p o s i t i o n   =   p r e v P o s ; 
 	 	 c a n o n b a l l s _ s h o t   =   0 ; 
 	 	 n r _ o f _ h i t s   =   0 ; 
                 ( ( G U I T e x t u r e ) ( G a m e O b j e c t . F i n d ( " K a r t a " ) ) . G e t C o m p o n e n t I n C h i l d r e n ( t y p e o f ( G U I T e x t u r e ) ) ) . e n a b l e d   =   t r u e ; 
 	 	 r e m i n d e r . S e t A c t i v e   ( f a l s e ) ; 
 	 } 
 
 	 / /   U s e   t h i s   f o r   i n i t i a l i z a t i o n 
 	 v o i d   S t a r t   ( )   { 
 	 	 c a n o n C a m e r a   =   G a m e O b j e c t . F i n d   ( " C a n o n C a m e r a " ) ; 
 	 	 r e m i n d e r   	 =   ( G a m e O b j e c t ) I n s t a n t i a t e   ( R e s o u r c e s . L o a d   ( " R e m i n d e r T e x t " ) ) ; 
 	 	 r e m i n d e r . t r a n s f o r m . p a r e n t   =   c a n o n C a m e r a . t r a n s f o r m . p a r e n t ; 
 	 	 e n d D i a g   =   ( G a m e O b j e c t ) I n s t a n t i a t e ( R e s o u r c e s . L o a d ( " Q u e s t E n d D i a l o g u e " ) ) ; 
 	 	 e n d D i a g . t r a n s f o r m . p a r e n t   =   c a n o n C a m e r a . t r a n s f o r m . p a r e n t ; 
 	 } 
 
 	 / / I n i t i a l i z e s   a r r o w s   o n   s c r e e n 
 	 v o i d   I n i t ( ) { 
 	 	 # i f   U N I T Y _ A N D R O I D 
 	 	 I n i t G U I ( ) ; 
 	 	 # e n d i f 
 
 	 } 
 
 	 / / R e s e t s   a f t e r   c o m p l e t i o n 
 	 v o i d   R e s e t ( ) { 
 	 	 c a n o n b a l l s _ s h o t   =   0 ; 
 	 	 r e l o a d _ t i m e r   =   0 ; 
 	 	 e n d _ t i m e r   =   0 ; 
 	 	 s h i p _ t i m e r   =   0 ; 
 	 } 
 
 	 / / I n i t i a l i z e s   G U I   f o r   a n d r o i d 
 	 v o i d   I n i t G U I ( ) { 
 
 	 	 G a m e O b j e c t   c a n o n G U I   =   n e w   G a m e O b j e c t   ( " C a n o n G U I " ) ; 
 	 	 
 	 	 G a m e O b j e c t   l a   =   n e w   G a m e O b j e c t   ( " A r r o w " ) ; 
 	 	 l e f t _ a r r o w   =   ( G U I T e x t u r e ) l a . A d d C o m p o n e n t   ( t y p e o f ( G U I T e x t u r e ) ) ; 
 	 	 l e f t _ a r r o w . t e x t u r e   =   a r r o w _ l e f t _ t e x t u r e ; 
 	 	 l e f t _ a r r o w . t r a n s f o r m . p o s i t i o n   =     n e w   V e c t o r 3   ( 0 . 6 5 f ,   0 . 1 f ,   0 ) ; 
 	 	 l e f t _ a r r o w . t r a n s f o r m . l o c a l S c a l e   =   n e w   V e c t o r 3   ( 0 . 1 f ,   0 . 1 f ,   0 ) ; 
 	 	 l e f t _ a r r o w . t r a n s f o r m . p a r e n t   =   c a n o n G U I . t r a n s f o r m ; 
 	 	 
 	 	 G a m e O b j e c t   r a   =   n e w   G a m e O b j e c t   ( " A r r o w " ) ; 
 	 	 r i g h t _ a r r o w   =   ( G U I T e x t u r e ) r a . A d d C o m p o n e n t   ( t y p e o f ( G U I T e x t u r e ) ) ; 
 	 	 r i g h t _ a r r o w . t e x t u r e   =   a r r o w _ r i g h t _ t e x t u r e ; 
 	 	 r i g h t _ a r r o w . t r a n s f o r m . p o s i t i o n   =     n e w   V e c t o r 3   ( 0 . 8 5 f ,   0 . 1 f ,   0 ) ; 
 	 	 r i g h t _ a r r o w . t r a n s f o r m . l o c a l S c a l e   =   n e w   V e c t o r 3   ( 0 . 1 f ,   0 . 1 f ,   0 ) ; 
 	 	 r i g h t _ a r r o w . t r a n s f o r m . p a r e n t   =   c a n o n G U I . t r a n s f o r m ; 
 	 	 
 	 	 G a m e O b j e c t   u a   =   n e w   G a m e O b j e c t   ( " A r r o w " ) ; 
 	 	 u p _ a r r o w   =   ( G U I T e x t u r e ) u a . A d d C o m p o n e n t   ( t y p e o f ( G U I T e x t u r e ) ) ; 
 	 	 u p _ a r r o w . t e x t u r e   =   a r r o w _ u p _ t e x t u r e ; 
 	 	 u p _ a r r o w . t r a n s f o r m . p o s i t i o n   =     n e w   V e c t o r 3   ( 0 . 7 5 f ,   0 . 2 f ,   0 ) ; 
 	 	 u p _ a r r o w . t r a n s f o r m . l o c a l S c a l e   =   n e w   V e c t o r 3   ( 0 . 1 f ,   0 . 1 1 f ,   0 ) ; 
 	 	 u p _ a r r o w . t r a n s f o r m . p a r e n t   =   c a n o n G U I . t r a n s f o r m ; 
 	 	 
 	 	 G a m e O b j e c t   d a   =   n e w   G a m e O b j e c t   ( " A r r o w " ) ; 
 	 	 d o w n _ a r r o w   =   ( G U I T e x t u r e ) d a . A d d C o m p o n e n t   ( t y p e o f ( G U I T e x t u r e ) ) ; 
 	 	 d o w n _ a r r o w . t e x t u r e   =   a r r o w _ d o w n _ t e x t u r e ; 
 	 	 d o w n _ a r r o w . t r a n s f o r m . p o s i t i o n   =     n e w   V e c t o r 3   ( 0 . 7 5 f ,   0 . 1 f ,   0 ) ; 
 	 	 d o w n _ a r r o w . t r a n s f o r m . l o c a l S c a l e   =   n e w   V e c t o r 3   ( 0 . 1 f ,   0 . 1 f ,   0 ) ; 
 	 	 d o w n _ a r r o w . t r a n s f o r m . p a r e n t   =   c a n o n G U I . t r a n s f o r m ; 
 	 	 
 	 	 G a m e O b j e c t   s h o o t _ g o   =   n e w   G a m e O b j e c t   ( " A r r o w " ) ; 
 	 	 s h o o t _ g u i   =   ( G U I T e x t u r e ) s h o o t _ g o . A d d C o m p o n e n t   ( t y p e o f ( G U I T e x t u r e ) ) ; 
 	 	 s h o o t _ g u i . t e x t u r e   =   f i r e _ t e x t u r e ; 
 	 	 s h o o t _ g u i . t r a n s f o r m . p o s i t i o n   =   n e w   V e c t o r 3   ( 0 . 5 f ,   0 . 1 f ,   0 ) ; 
 	 	 s h o o t _ g u i . t r a n s f o r m . l o c a l S c a l e   =   n e w   V e c t o r 3   ( 0 . 1 f ,   0 . 1 f ,   0 ) ; 
 	 	 s h o o t _ g u i . t r a n s f o r m . p a r e n t   =   c a n o n G U I . t r a n s f o r m ; 
 	 	 
 	 	 g u i L i s t   [ 0 ]   =   l e f t _ a r r o w ; 
 	 	 g u i L i s t   [ 1 ]   =   r i g h t _ a r r o w ; 
 	 	 g u i L i s t   [ 2 ]   =   u p _ a r r o w ; 
 	 	 g u i L i s t   [ 3 ]   =   d o w n _ a r r o w ; 
 	 	 g u i L i s t   [ 4 ]   =   s h o o t _ g u i ; 
 	 } 
 	 
 	 / /   U p d a t e   i s   c a l l e d   o n c e   p e r   f r a m e 
 	 v o i d   U p d a t e   ( )   { 
 	 	 i f   ( q u e s t A c t i v e )   { 
 
 	 	 	 # i f   U N I T Y _ S T A N D A L O N E _ W I N 
 	 	 	 U p d a t e K e y b o a r d ( ) ; 
 	 	 	 # e n d i f 
 	 	 	 # i f   U N I T Y _ A N D R O I D 
 	 	 	 U p d a t e T o u c h ( ) ; 
 	 	 	 # e n d i f 
 
 	 	 	 / / A v s l u t a   s p e l 
 	 	 	 i f ( n r _ o f _ h i t s   >   0   | |   c a n o n b a l l s _ s h o t   > =   T O T A L _ C A N O N B A L L S ) 
 	 	 	 { 
 	 	 	 	 U p d a t e E n d T i m e r ( ) ; 
 	 	 	 } 
 	 	 	 / / - - - 
 
 	 	 	 i f ( c a n o n b a l l _ i n _ a i r ) 
 	 	 	 	 U p d a t e C a n o n b a l l s ( ) ; 
 
 	 	 	 s h i p _ t i m e r   + =   T i m e . d e l t a T i m e ; 
 	 	 	 i f ( s h i p _ t i m e r   > =   M I S S E D _ S H I P _ T I M E R ) 
 	 	 	 	 T r i g g e r F i n i s h ( f a l s e ) ; 
 	 	 } 
 	 } 
 
 	 / / K e y b o a r d   i n p u t   f o r   c o n t r o l l i n g   c a n o n   ( P C ) 
 	 v o i d   U p d a t e K e y b o a r d ( ) { 
 	 	 / / S v � n g   v � n s t e r 
 	 	 i f   ( I n p u t . G e t K e y   ( K e y C o d e . A )   & &   s t a r t _ s i d e _ r o t a t i o n . y   >   - 1 0 f )   { 
 	 	 	 c a n o n . t r a n s f o r m . R o t a t e ( - V e c t o r 3 . u p   *   R O T A T I O N _ S P E E D   *   T i m e . d e l t a T i m e ) ; 
 	 	 	 s t a r t _ s i d e _ r o t a t i o n   + =   - V e c t o r 3 . u p   *   R O T A T I O N _ S P E E D   *   T i m e . d e l t a T i m e ; 
 	 	 } 
 	 	 / / S v � n g   h � g e r 
 	 	 i f   ( I n p u t . G e t K e y   ( K e y C o d e . D )   & &   s t a r t _ s i d e _ r o t a t i o n . y   <   1 0 f ) { 
 	 	 	 c a n o n . t r a n s f o r m . R o t a t e ( V e c t o r 3 . u p   *   R O T A T I O N _ S P E E D   *   T i m e . d e l t a T i m e ) ; 
 	 	 	 s t a r t _ s i d e _ r o t a t i o n   + =   V e c t o r 3 . u p   *   R O T A T I O N _ S P E E D   *   T i m e . d e l t a T i m e ; 
 	 	 } 
 	 	 / / S v � n g   u p p 
 	 	 i f   ( I n p u t . G e t K e y   ( K e y C o d e . W )   & &   s t a r t _ u p _ r o t a t i o n . x   >   - 1 0 f ) { 
 	 	 	 c a n o n P i p e . t r a n s f o r m . R o t a t e ( V e c t o r 3 . l e f t   *   R O T A T I O N _ S P E E D   *   T i m e . d e l t a T i m e ) ;   
 	 	 	 s t a r t _ u p _ r o t a t i o n   + =   V e c t o r 3 . l e f t   *   R O T A T I O N _ S P E E D   *   T i m e . d e l t a T i m e ; 
 	 	 } 
 	 	 / / S v � n g   n e r 
 	 	 i f   ( I n p u t . G e t K e y   ( K e y C o d e . S )   & &   s t a r t _ u p _ r o t a t i o n . x   <   0 f ) { 
 	 	 	 c a n o n P i p e . t r a n s f o r m . R o t a t e ( V e c t o r 3 . r i g h t   *   R O T A T I O N _ S P E E D   *   T i m e . d e l t a T i m e ) ; 
 	 	 	 s t a r t _ u p _ r o t a t i o n   + =   V e c t o r 3 . r i g h t   *   R O T A T I O N _ S P E E D   *   T i m e . d e l t a T i m e ; 
 	 	 } 
 	 	 / / S k j u t 
 	 	 i f ( I n p u t . G e t K e y D o w n ( K e y C o d e . S p a c e )   & &   c a n o n b a l l s _ s h o t   < =   T O T A L _ C A N O N B A L L S   & &   ! c a n o n b a l l _ i n _ a i r ) { 
 	 	 	 F i r e ( ) ; 
 	 	 } 
 	 } 
 
 	 / / T o u c h   i n p u t   f o r   c o n t r o l l i n g   c a n o n   ( A n d r o i d ) 
 	 v o i d   U p d a t e T o u c h ( ) { 
 
 	 	 i f   ( I n p u t . t o u c h e s . L e n g t h   = =   0 )   { 
 
 	 	 }   
 	 	 e l s e   { 
 	 	 	 f o r ( i n t   j   =   0 ;   j   <   5 ;   j + + ) 
 	 	 	 	 f o r   ( i n t   i   =   0 ;   i   <   I n p u t . t o u c h C o u n t ;   i + + )   { 
 	 	 	 	 i f   ( g u i L i s t [ j ]   ! =   n u l l   & &   ( g u i L i s t [ j ] . H i t T e s t   ( I n p u t . G e t T o u c h   ( i ) . p o s i t i o n ) ) )   { 
 	 	 	 	 	 	 / / i f   c u r r e n t   t o u c h   h i t s   o u r   g u i t e x t u r e ,   r u n   t h i s   c o d e 
 	 	 	 	 	 i f   ( I n p u t . G e t T o u c h   ( i ) . p h a s e   = =   T o u c h P h a s e . B e g a n   | |   I n p u t . G e t T o u c h   ( i ) . p h a s e   = =   T o u c h P h a s e . S t a t i o n a r y )   { 
 	 	 	 	 	 	 i f ( j   = =   0     & &   s t a r t _ s i d e _ r o t a t i o n . y   >   - 1 0 f ) { 	 	 / / V � N S T E R 
 	 	 	 	 	 	 	 c a n o n . t r a n s f o r m . R o t a t e ( - V e c t o r 3 . u p   *   R O T A T I O N _ S P E E D   *   T i m e . d e l t a T i m e ) ; 
 	 	 	 	 	 	 	 s t a r t _ s i d e _ r o t a t i o n   + =   - V e c t o r 3 . u p   *   R O T A T I O N _ S P E E D   *   T i m e . d e l t a T i m e ; 
 	 	 	 	 	 	 } 
 	 	 	 	 	 	 i f ( j   = =   1   & &   s t a r t _ s i d e _ r o t a t i o n . y   <   1 0 f ) { 	 	 	 / / H � G E R 
 	 	 	 	 	 	 	 c a n o n . t r a n s f o r m . R o t a t e ( V e c t o r 3 . u p   *   R O T A T I O N _ S P E E D   *   T i m e . d e l t a T i m e ) ; 
 	 	 	 	 	 	 	 s t a r t _ s i d e _ r o t a t i o n   + =   V e c t o r 3 . u p   *   R O T A T I O N _ S P E E D   *   T i m e . d e l t a T i m e ; 
 	 	 	 	 	 	 } 
 	 	 	 	 	 	 i f ( j   = =   2   & &   s t a r t _ u p _ r o t a t i o n . x   >   - 1 0 f ) { 	 	 	 / / U P P 
 	 	 	 	 	 	 	 c a n o n P i p e . t r a n s f o r m . R o t a t e ( V e c t o r 3 . l e f t   *   R O T A T I O N _ S P E E D   *   T i m e . d e l t a T i m e ) ;   
 	 	 	 	 	 	 	 s t a r t _ u p _ r o t a t i o n   + =   V e c t o r 3 . l e f t   *   R O T A T I O N _ S P E E D   *   T i m e . d e l t a T i m e ; 
 	 	 	 	 	 	 } 
 	 	 	 	 	 	 i f ( j   = =   3   & &   s t a r t _ u p _ r o t a t i o n . x   <   0 f ) { 	 	 	 	 / / N E R 
 	 	 	 	 	 	 	 c a n o n P i p e . t r a n s f o r m . R o t a t e ( V e c t o r 3 . r i g h t   *   R O T A T I O N _ S P E E D   *   T i m e . d e l t a T i m e ) ; 
 	 	 	 	 	 	 	 s t a r t _ u p _ r o t a t i o n   + =   V e c t o r 3 . r i g h t   *   R O T A T I O N _ S P E E D   *   T i m e . d e l t a T i m e ; 
 	 	 	 	 	 	 } 
 	 	 	 	 	 	 i f ( j   = =   4   & &   c a n o n b a l l s _ s h o t   < =   T O T A L _ C A N O N B A L L S   & &   ! c a n o n b a l l _ i n _ a i r ) { 	 / / S K J U T 
 	 	 	 	 	 	 	 F i r e ( ) ; 
 	 	 	 	 	 	 } 	 
 	 	 	 	 	 } 
 	 	 	 	 } 
 	 	 	 } 
 	 	 } 
 	 } 
 
 	 / / U p d a t e s   w h e n   p l a y e r   h a s   e i t h e r   h i t   t h e   s h i p   o r   i s   o u t   o f   c a n n o n b a l l s 
 	 v o i d   U p d a t e E n d T i m e r ( ) { 
 	 	 e n d _ t i m e r   + =   T i m e . d e l t a T i m e ; 
 	 	 i f   ( e n d _ t i m e r   >   E N D _ T I M E R _ C O M P L E T E   & &   n r _ o f _ h i t s   >   0 )   { 
 	 	 	 R e s e t   ( ) ; 
 	 	 	 T r i g g e r F i n i s h   ( t r u e ) ; 
 	 	 }   e l s e   i f   ( e n d _ t i m e r   >   E N D _ T I M E R _ F A I L   & &   n r _ o f _ h i t s   = =   0 )   { 
 	 	 	 R e s e t   ( ) ; 
 	 	 	 T r i g g e r F i n i s h   ( f a l s e ) ; 
 	 	 } 
 	 } 
 
 	 / / C a l l e d   w h e n   p l a y e r   p r e s s e s   s h o o t s   t h e   c a n o n 
 	 p r i v a t e   v o i d   F i r e ( ) { 
 	 	 s m o k e . S t o p   ( ) ; 
 	 	 s m o k e . C l e a r   ( ) ; 
 	 	 c a n o n b a l l s _ s h o t + + ; 
 	 	 c a n o n b a l l _ i n _ a i r   =   t r u e ; 
 	 	 c a n o n B a l l   =   I n s t a n t i a t e   ( R e s o u r c e s . L o a d   ( " C a n o n B a l l " ) )   a s   G a m e O b j e c t ; 
 	 	 c a n o n B a l l . t r a n s f o r m . p o s i t i o n   =   c a n o n M u z z l e . t r a n s f o r m . p o s i t i o n ; 
 	 	 c a n o n B a l l . r i g i d b o d y . A d d F o r c e   ( ( c a n o n M u z z l e . t r a n s f o r m . p o s i t i o n   -   c a n o n B a s e . t r a n s f o r m . p o s i t i o n ) . n o r m a l i z e d   *   4 0 0 0 f ) ; 
 	 	 s m o k e . t r a n s f o r m . p o s i t i o n   =   c a n o n M u z z l e . t r a n s f o r m . p o s i t i o n ; 
 	 	 s m o k e . P l a y   ( ) ; 
 	 } 
 
 
 	 / / U p d a t e s   r e l o a d   t i m e r   a n d   d e s t r o y s   c a n o n b a l l 
 	 p r i v a t e   v o i d   U p d a t e C a n o n b a l l s ( ) { 
 	 	 r e l o a d _ t i m e r   + =   T i m e . d e l t a T i m e ; 
 
 	 	 i f   ( r e l o a d _ t i m e r   > =   R E L O A D _ T I M E )   { 
 	 	 	 D e s t r o y ( c a n o n B a l l ) ; 	 
 	 	 	 r e l o a d _ t i m e r   =   0 ; 
 	 	 	 c a n o n b a l l _ i n _ a i r   =   f a l s e ; 
 	 	 } 
 	 } 
 
 	 / / C a l l e d   f r o m   C a n o n B a l l S c r i p t   w h e n   i t   h i t ' s   t h e   b o a t 
 	 p u b l i c   v o i d   C a n o n B a l l T r i g g e r ( b o o l   h i t ) { 
 	 	 i f   ( h i t )   { 
 	 	 	 n r _ o f _ h i t s + + ; 
 	 	 	 S h i p C o l l i s i o n ( ) ; 
 	 	 } 
 	 } 
 
 	 / / C a l l e d   w h e n   s h i p   g e t s   h i t 
 	 p r i v a t e   v o i d   S h i p C o l l i s i o n ( ) { 
 	 	 S h i p S c r i p t   s c r i p t   =   s h i p . G e t C o m p o n e n t   ( t y p e o f ( S h i p S c r i p t ) )   a s   S h i p S c r i p t ; 
 	 	 i f   ( s c r i p t   ! =   n u l l )   { 
 	 	 	 s c r i p t . S p e e d   =   n e w   V e c t o r 3   ( 0 . 0 3 f ,   - 0 . 0 2 f ,   0 . 0 3 f ) ; 
 	 	 	 s c r i p t . h i t   =   t r u e ; 
 	 	 } 
 
 	 } 
 
 	 / / D r a w s   G U I   e l e m e n t s 
 	 v o i d   O n G U I ( ) { 
 	 	 i f   ( q u e s t A c t i v e )   { 
 	 	 	 i n t   s h   =   S c r e e n . h e i g h t ; 
 	 	 	 f o r ( i n t   i   =   0 ;   i   <   T O T A L _ C A N O N B A L L S   -   c a n o n b a l l s _ s h o t ;   i + + ) { 
 	 	 	 	 G U I . D r a w T e x t u r e ( n e w   R e c t ( 1 0   +   3 0   *   i , s h   -   7 0 , 6 0 , 6 0 ) , c a n o n b a l l _ t e x t u r e ,   S c a l e M o d e . S c a l e T o F i t ) ; 
 	 	 	 } 	 
 
 	 	 	 i f ( c a n o n b a l l _ i n _ a i r ) { 
 	 	 	 	 f l o a t   w   =   ( ( R E L O A D _ T I M E   -   r e l o a d _ t i m e r )   /   R E L O A D _ T I M E )   *   2 0 0 ; 
 	 	 	 	 G U I . D r a w T e x t u r e ( n e w   R e c t ( S c r e e n . w i d t h / 2   -   1 0 0 , s h / 1 . 1 f ,   w ,   1 5 ) ,   r e l o a d b a r _ t e x t u r e ) ; 
 	 	 	 } 
 
 	 	 } 
 	 } 	 
 } 
 