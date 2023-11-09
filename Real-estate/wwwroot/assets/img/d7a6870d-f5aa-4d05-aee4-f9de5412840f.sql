use RealState;
--Property 1
insert into properties (UserId,PropertyName,PropertyPrice,PropertyDescription,PropertyVideo,DateTime,Area,BedRooms,Garage,BathRooms,CityId,StatusId,CatagoryId,CoverImageUrl) 
values('37651fd4-b224-48a8-b139-710883a349af','Avenue Prime New',533000,'Nulla quis dapibus nisl. Suspendisse ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies','https://www.youtube.com/embed/y9j-BL5ocW8',GETDATE(),1130,300,3,400,1,1,1,'/assets/img/demo/property-2.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Avenue Prime New'),'/assets/img/property-1/1-2.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Avenue Prime New'),'/assets/img/property-1/1-1.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Avenue Prime New'),'/assets/img/property-1/1-3.jpg');

insert into Features (PropertyId,SwimmingPool,Stories,EmergencyExit,FirePlace,LaundryRoom,JogPath,Ceilings,DoubleSinks,HurricaneShutters) values((select PropertyId from properties where PropertyName='Avenue Prime New'),1,0,1,0,1,0,1,0,1);
insert into AdditionalDetails(PropertId,PropertyId,WaterFront,BuiltIn,Parking,[View],WaterFrontDescription) values((select PropertyId from properties where PropertyName='Avenue Prime New'),1,1,2022,55,23,11);

--property 2
insert into properties (UserId,PropertyName,PropertyPrice,PropertyDescription,PropertyVideo,DateTime,Area,BedRooms,Garage,BathRooms,CityId,StatusId,CatagoryId,CoverImageUrl) 
values('37651fd4-b224-48a8-b139-710883a349af','Platinum Amaltas New',683000,'Nulla quis dapibus nisl. Suspendisse ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies','https://www.youtube.com/embed/y9j-BL5ocW8',GETDATE(),130,300,3,400,2,1,2,'/assets/img/demo/property-4.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Platinum Amaltas New'),'/assets/img/property-1/2-2.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Platinum Amaltas New'),'/assets/img/property-1/2-1.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Platinum Amaltas New'),'/assets/img/property-1/2-3.jpg');

insert into Features (PropertyId,SwimmingPool,Stories,EmergencyExit,FirePlace,LaundryRoom,JogPath,Ceilings,DoubleSinks,HurricaneShutters) values((select PropertyId from properties where PropertyName='Platinum Amaltas New'),1,0,1,0,1,0,1,0,1);
insert into AdditionalDetails(PropertId,PropertyId,WaterFront,BuiltIn,Parking,[View],WaterFrontDescription) values((select PropertyId from properties where PropertyName='Platinum Amaltas New'),1,1,2022,55,23,11);
--property -3
insert into properties (UserId,PropertyName,PropertyPrice,PropertyDescription,PropertyVideo,DateTime,Area,BedRooms,Garage,BathRooms,CityId,StatusId,CatagoryId,CoverImageUrl) 
values('37651fd4-b224-48a8-b139-710883a349af','Guman ShreeDam New',683000,'Nulla quis dapibus nisl. Suspendisse ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies','https://www.youtube.com/embed/y9j-BL5ocW8',GETDATE(),130,300,3,400,2,1,2,'/assets/img/demo/property-3.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Guman ShreeDam New'),'/assets/img/property-1/3-2.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Guman ShreeDam New'),'/assets/img/property-1/3-1.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Guman ShreeDam New'),'/assets/img/property-1/3-3.jpg');

insert into Features (PropertyId,SwimmingPool,Stories,EmergencyExit,FirePlace,LaundryRoom,JogPath,Ceilings,DoubleSinks,HurricaneShutters) values((select PropertyId from properties where PropertyName='Guman ShreeDam New'),1,0,1,0,1,0,1,0,1);
insert into AdditionalDetails(PropertId,PropertyId,WaterFront,BuiltIn,Parking,[View],WaterFrontDescription) values((select PropertyId from properties where PropertyName='Guman ShreeDam New'),1,1,2022,55,23,11);

--property 4
insert into properties (UserId,PropertyName,PropertyPrice,PropertyDescription,PropertyVideo,DateTime,Area,BedRooms,Garage,BathRooms,CityId,StatusId,CatagoryId,CoverImageUrl) 
values('37651fd4-b224-48a8-b139-710883a349af','Emprire State New',683000,'Nulla quis dapibus nisl. Suspendisse ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies','https://www.youtube.com/embed/y9j-BL5ocW8',GETDATE(),130,300,3,400,2,1,2,'/assets/img/demo/property-1.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Emprire State New'),'/assets/img/property-1/2-2.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Emprire State New'),'/assets/img/property-1/2-1.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Emprire State New'),'/assets/img/property-1/2-3.jpg');

insert into Features (PropertyId,SwimmingPool,Stories,EmergencyExit,FirePlace,LaundryRoom,JogPath,Ceilings,DoubleSinks,HurricaneShutters) values((select PropertyId from properties where PropertyName='Emprire State New'),1,0,1,0,1,0,1,0,1);
insert into AdditionalDetails(PropertId,PropertyId,WaterFront,BuiltIn,Parking,[View],WaterFrontDescription) values((select PropertyId from properties where PropertyName='Emprire State New'),1,1,2022,55,23,11);
--property -5
insert into properties (UserId,PropertyName,PropertyPrice,PropertyDescription,PropertyVideo,DateTime,Area,BedRooms,Garage,BathRooms,CityId,StatusId,CatagoryId,CoverImageUrl) 
values('37651fd4-b224-48a8-b139-710883a349af','Kbc Sunshine New',623000,'Nulla quis dapibus nisl. Suspendisse ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies','https://www.youtube.com/embed/y9j-BL5ocW8',GETDATE(),130,300,3,400,2,1,1,'/assets/img/demo/property-2.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Kbc Sunshine New'),'/assets/img/property-1/3-2.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Kbc Sunshine New'),'/assets/img/property-1/3-1.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Kbc Sunshine New'),'/assets/img/property-1/3-3.jpg');

insert into Features (PropertyId,SwimmingPool,Stories,EmergencyExit,FirePlace,LaundryRoom,JogPath,Ceilings,DoubleSinks,HurricaneShutters) values((select PropertyId from properties where PropertyName='Kbc Sunshine New'),1,0,1,0,1,0,1,0,1);
insert into AdditionalDetails(PropertId,PropertyId,WaterFront,BuiltIn,Parking,[View],WaterFrontDescription) values((select PropertyId from properties where PropertyName='Kbc Sunshine New'),1,1,2022,55,23,11);


--property-6
insert into properties (UserId,PropertyName,PropertyPrice,PropertyDescription,PropertyVideo,DateTime,Area,BedRooms,Garage,BathRooms,CityId,StatusId,CatagoryId,CoverImageUrl) 
values('37651fd4-b224-48a8-b139-710883a349af','Kedie the Kothi New',23000,'Nulla quis dapibus nisl. Suspendisse ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies','https://www.youtube.com/embed/y9j-BL5ocW8',GETDATE(),130,300,3,400,2,1,1,'/assets/img/demo/property-5.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Kedie the Kothi New'),'/assets/img/property-1/1-2.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Kedie the Kothi New'),'/assets/img/property-1/1-1.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Kedie the Kothi New'),'/assets/img/property-1/1-3.jpg');

insert into Features (PropertyId,SwimmingPool,Stories,EmergencyExit,FirePlace,LaundryRoom,JogPath,Ceilings,DoubleSinks,HurricaneShutters) values((select PropertyId from properties where PropertyName='Kedie the Kothi New'),1,0,1,0,1,0,1,0,1);
insert into AdditionalDetails(PropertId,PropertyId,WaterFront,BuiltIn,Parking,[View],WaterFrontDescription) values((select PropertyId from properties where PropertyName='Kedie the Kothi New'),1,1,2022,55,23,11);

--property-7
insert into properties (UserId,PropertyName,PropertyPrice,PropertyDescription,PropertyVideo,DateTime,Area,BedRooms,Garage,BathRooms,CityId,StatusId,CatagoryId,CoverImageUrl) 
values('37651fd4-b224-48a8-b139-710883a349af','The Century Elite New',593000,'Nulla quis dapibus nisl. Suspendisse ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies','https://www.youtube.com/embed/y9j-BL5ocW8',GETDATE(),130,300,3,400,2,1,1,'/assets/img/demo/property-1.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='The Century Elite New'),'/assets/img/property-1/2-2.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='The Century Elite New'),'/assets/img/property-1/2-1.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='The Century Elite New'),'/assets/img/property-1/2-3.jpg');

insert into Features (PropertyId,SwimmingPool,Stories,EmergencyExit,FirePlace,LaundryRoom,JogPath,Ceilings,DoubleSinks,HurricaneShutters) values((select PropertyId from properties where PropertyName='The Century Elite New'),1,0,1,0,1,0,1,0,1);
insert into AdditionalDetails(PropertId,PropertyId,WaterFront,BuiltIn,Parking,[View],WaterFrontDescription) values((select PropertyId from properties where PropertyName='The Century Elite New'),1,1,2022,55,23,11);

--property-8
insert into properties (UserId,PropertyName,PropertyPrice,PropertyDescription,PropertyVideo,DateTime,Area,BedRooms,Garage,BathRooms,CityId,StatusId,CatagoryId,CoverImageUrl) 
values('37651fd4-b224-48a8-b139-710883a349af','Jal Mahal New',613000,'Nulla quis dapibus nisl. Suspendisse ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies','https://www.youtube.com/embed/y9j-BL5ocW8',GETDATE(),130,300,3,400,2,1,1,'/assets/img/demo/property-3.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Jal Mahal New'),'/assets/img/property-1/1-2.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Jal Mahal New'),'/assets/img/property-1/1-1.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Jal Mahal New'),'/assets/img/property-1/1-3.jpg');

insert into Features (PropertyId,SwimmingPool,Stories,EmergencyExit,FirePlace,LaundryRoom,JogPath,Ceilings,DoubleSinks,HurricaneShutters) values((select PropertyId from properties where PropertyName='Jal Mahal New'),1,0,1,0,1,0,1,0,1);
insert into AdditionalDetails(PropertId,PropertyId,WaterFront,BuiltIn,Parking,[View],WaterFrontDescription) values((select PropertyId from properties where PropertyName='Jal Mahal New'),1,1,2022,55,23,11);

--property-10
insert into properties (UserId,PropertyName,PropertyPrice,PropertyDescription,PropertyVideo,DateTime,Area,BedRooms,Garage,BathRooms,CityId,StatusId,CatagoryId,CoverImageUrl) 
values('37651fd4-b224-48a8-b139-710883a349af','Premnum Avenue Street',73000,'Nulla quis dapibus nisl. Suspendisse ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies','https://www.youtube.com/embed/y9j-BL5ocW8',GETDATE(),130,300,3,400,2,1,1,'/assets/img/demo/property-6.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Premnum Avenue Street'),'/assets/img/property-1/3-2.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Premnum Avenue Street'),'/assets/img/property-1/3-1.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Premnum Avenue Street'),'/assets/img/property-1/3-3.jpg');

insert into Features (PropertyId,SwimmingPool,Stories,EmergencyExit,FirePlace,LaundryRoom,JogPath,Ceilings,DoubleSinks,HurricaneShutters) values((select PropertyId from properties where PropertyName='Premnum Avenue Street'),1,0,1,0,1,0,1,0,1);
insert into AdditionalDetails(PropertId,PropertyId,WaterFront,BuiltIn,Parking,[View],WaterFrontDescription) values((select PropertyId from properties where PropertyName='Premnum Avenue Street'),1,1,2022,55,23,11);

--property-11
insert into properties (UserId,PropertyName,PropertyPrice,PropertyDescription,PropertyVideo,DateTime,Area,BedRooms,Garage,BathRooms,CityId,StatusId,CatagoryId,CoverImageUrl) 
values('37651fd4-b224-48a8-b139-710883a349af','Premnum Guman Shreedan',53000,'Nulla quis dapibus nisl. Suspendisse ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies','https://www.youtube.com/embed/y9j-BL5ocW8',GETDATE(),130,300,3,400,2,1,1,'/assets/img/demo/property-2.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Premnum Guman Shreedan'),'/assets/img/property-1/3-2.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Premnum Guman Shreedan'),'/assets/img/property-1/3-1.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Premnum Guman Shreedan'),'/assets/img/property-1/3-3.jpg');

insert into Features (PropertyId,SwimmingPool,Stories,EmergencyExit,FirePlace,LaundryRoom,JogPath,Ceilings,DoubleSinks,HurricaneShutters) values((select PropertyId from properties where PropertyName='Premnum Guman Shreedan'),1,0,1,0,1,0,1,0,1);
insert into AdditionalDetails(PropertId,PropertyId,WaterFront,BuiltIn,Parking,[View],WaterFrontDescription) values((select PropertyId from properties where PropertyName='Premnum Guman Shreedan'),1,1,2022,55,23,11);

--property-12
insert into properties (UserId,PropertyName,PropertyPrice,PropertyDescription,PropertyVideo,DateTime,Area,BedRooms,Garage,BathRooms,CityId,StatusId,CatagoryId,CoverImageUrl) 
values('37651fd4-b224-48a8-b139-710883a349af','Premium Empire State',83000,'Nulla quis dapibus nisl. Suspendisse ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies Nulla quis dapibus nisl. Suspendisse ultricies commodo arcu nec pretium. Nullam sed arcu ultricies','https://www.youtube.com/embed/y9j-BL5ocW8',GETDATE(),130,300,3,400,2,1,1,'/assets/img/demo/property-6.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Premium Empire State'),'/assets/img/property-1/1-2.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Premium Empire State'),'/assets/img/property-1/1-1.jpg');
insert into PropertyImages (PropertyId,ImageUrl) values((select PropertyId from properties where PropertyName='Premium Empire State'),'/assets/img/property-1/1-3.jpg');

insert into Features (PropertyId,SwimmingPool,Stories,EmergencyExit,FirePlace,LaundryRoom,JogPath,Ceilings,DoubleSinks,HurricaneShutters) values((select PropertyId from properties where PropertyName='Premium Empire State'),1,0,1,0,1,0,1,0,1);
insert into AdditionalDetails(PropertId,PropertyId,WaterFront,BuiltIn,Parking,[View],WaterFrontDescription) values((select PropertyId from properties where PropertyName='Premium Empire State'),1,1,2022,55,23,11);
